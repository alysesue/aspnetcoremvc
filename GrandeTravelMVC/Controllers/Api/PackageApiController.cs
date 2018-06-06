using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravelMVC.Models;
using GrandeTravelMVC.Services;
using GrandeTravelMVC.ViewModels;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravelMVC.Controllers.Api
{
    public class PackageApiController : Controller
    {
        private IDataService<Location> _locationDataService;
        private IDataService<Package> _packageDataService;

        public PackageApiController(IDataService<Package> packageService,
                                 IDataService<Location> locationService)
        {
            _packageDataService = packageService;
            _locationDataService = locationService;
        }

        [HttpGet("api/packagegetall")]
        public JsonResult GetAllPackages()
        {
            try
            {
                IEnumerable<Package> packageList = _packageDataService.GetAll();
                return Json(packageList);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = e.Message });
            }
        }

        [HttpGet("api/packagegetbylocation")]
        public JsonResult GetPackageByLocation(string locationName)
        {
            try
            {
                IEnumerable<Package> packageList = _packageDataService.Query(p => p.Location.ToLower() == locationName.ToLower());
                if(packageList != null)
                {
                    return Json(packageList);
                }
                else
                {
                    return Json(new { message = "cannot find any packages" });
                }

            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = e.Message });
            }
        }
    }
}

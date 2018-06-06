using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravelMVC.Models;
using GrandeTravelMVC.Services;
using GrandeTravelMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravelMVC.Controllers
{
    public class LocationController : Controller
    {
        private IDataService<Location> _locationDataService;
        private IDataService<Package> _packageDataService;

        public LocationController(IDataService<Package> packageService,
                                 IDataService<Location> locationService)
        {
            _packageDataService = packageService;
            _locationDataService = locationService;
        }

        [HttpGet]
        public IActionResult Details(int id, string name)
        {
            TempData["locationId"] = id.ToString();

            //Package package = _packageDataService.GetSingle(p => p.PackageId == id);

            //TempData["locationName"] = name;

            Location location = _locationDataService.GetSingle(p => p.LocationId == id);
            IEnumerable<Package> packageList = _packageDataService.Query(p => p.LocationId == id && p.IsAvailable == true);

            LocationDetailsViewModel vm = new LocationDetailsViewModel
            {
                Name = location.Name,
                Description = location.Description,
                Picture = location.Picture,
                Packages = packageList
            };

            return View(vm);
        }
    }
}

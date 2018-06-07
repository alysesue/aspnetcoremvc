using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravelMVC.Controllers.Api
{
    public class AccountApiController : Controller
    {
        //[HttpGet("api/packagegetall")]
        //public JsonResult GetAllPackages()
        //{
        //    try
        //    {
        //        IEnumerable<Package> packageList = _packageDataService.GetAll();
        //        return Json(packageList);
        //    }
        //    catch (Exception e)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return Json(new { message = e.Message });
        //    }
        //}

        
        public string Protected()
        {
            return "Only if you have a valid token";
        }

    }
}

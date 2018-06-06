using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravelMVC.Models;
using GrandeTravelMVC.Services;
using GrandeTravelMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GrandeTravelMVC.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Location> _locationDataService;
        private IDataService<Package> _packageDataService;
        public HomeController(IDataService<Location> locationService,
                              IDataService<Package> packageService)
        {
            _locationDataService = locationService;
            _packageDataService = packageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Location> locationList = _locationDataService.GetAll();
            IEnumerable<Package> packageList = _packageDataService.GetAll().Where(p => p.IsAvailable == true);

            HomeIndexViewModel vm = new HomeIndexViewModel
            {
                Locations = locationList,
                Packages = packageList,
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Help()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Terms()
        {
            return View();
        }
    }
}

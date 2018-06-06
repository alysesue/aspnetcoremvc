using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravelMVC.Models;
using GrandeTravelMVC.Services;
using GrandeTravelMVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GrandeTravelMVC.Controllers
{
    public class PackageController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Location> _locationDataService;
        private IDataService<Package> _packageDataService;
        private IDataService<Feedback> _feedbackDataService;
        private IHostingEnvironment _environment;

        public PackageController(UserManager<IdentityUser> userManager,
                                 IDataService<Package> packageService,
                                 IDataService<Location> locationService,
                                 IDataService<Feedback> feedbackService,
                                 IHostingEnvironment environment)
        {
            _userManagerService = userManager;
            _packageDataService = packageService;
            _locationDataService = locationService;
            _feedbackDataService = feedbackService;
            _environment = environment;
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public IActionResult Create(int id)
        {
            //is there a way to optimise to store location id and name as temp data so do not need to pull from database 
            //int locationId = int.Parse(TempData["locationId"].ToString());
            Location location = _locationDataService.GetSingle(p => p.LocationId == id);

            PackageCreateViewModel vm = new PackageCreateViewModel
            {
                //LocationId = locationId,
                LocationId = location.LocationId,
                LocationName = location.Name
            };

            //IEnumerable<Location> locationList = _locationDataService.GetAll().OrderBy(p => p.Name);

            //PackageCreateViewModel vm = new PackageCreateViewModel
            //{
            //    LocationList = new SelectList(locationList, "Id", "Value")
            //};
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Create(PackageCreateViewModel vm, IFormFile file)
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                Package existingPackage = _packageDataService.GetSingle(p => p.Name == vm.PackageName);
                if (existingPackage == null)
                {
                    Package package = new Package
                    {
                        Name = vm.PackageName,
                        Price = vm.Price,
                        Location = vm.LocationName,
                        Description = vm.Description,
                        IsAvailable = true,
                        LocationId = vm.LocationId,
                        UserId = user.Id
                    };

                    if (file != null)
                    {
                        var serverPath = Path.Combine(_environment.WebRootPath, "uploads/package");
                        Directory.CreateDirectory(Path.Combine(serverPath, User.Identity.Name));
                        string fileName = FileNameHelper.GetNameFormatted(Path.GetFileName(file.FileName));
                        using (var fileStream = new FileStream(Path.Combine(serverPath, User.Identity.Name, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        package.Picture = User.Identity.Name + "/" + fileName;
                    }
                    else if (file == null)
                    {
                        package.Picture = "apackagegen.jpg";
                    }

                    _packageDataService.Create(package);
                    return RedirectToAction("Details", "Package", new { id = package.PackageId });
                }
                else
                {
                    ViewBag.MyMessage = "Package name exists. please change the name";
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> Update(int id)
        //{
        //    IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

        //    Package package = _packageDataService.GetSingle(p => p.PackageId == id);

        //    if (package.UserId == user.Id)
        //    {
        //        PackageUpdateViewModel vm = new PackageUpdateViewModel
        //        {
        //            PackageId = package.PackageId,
        //            PackageName = package.Name,
        //            LocationName = package.Location,
        //            Price = package.Price,
        //            Description = package.Description,
        //            Picture = package.Description,
        //            IsAvailable = package.IsAvailable,
        //            LocationId = package.LocationId,
        //            UserId = package.UserId
        //        };
        //        return View(vm);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Denied", "Account");
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(PackageUpdateViewModel vm, IFormFile file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Package package = new Package
        //        {
        //            PackageId = vm.PackageId,
        //            Name = vm.PackageName,
        //            Location = vm.LocationName,
        //            Price = vm.Price,
        //            Description = vm.Description,
        //            IsAvailable = vm.IsAvailable,
        //            LocationId = vm.LocationId,
        //            UserId = vm.UserId
        //        };

        //        if (file != null)
        //        {
        //            var serverPath = Path.Combine(_environment.WebRootPath, "uploads/package");
        //            Directory.CreateDirectory(Path.Combine(serverPath, User.Identity.Name));
        //            string fileName = FileNameHelper.GetNameFormatted(Path.GetFileName(file.FileName));
        //            using (var fileStream = new FileStream(Path.Combine(serverPath, User.Identity.Name, fileName), FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }
        //            package.Picture = User.Identity.Name + "/" + fileName;
        //        }

        //        _packageDataService.Update(package);
        //        return RedirectToAction("Details", "Package", new { id = package.PackageId });
        //    }
        //    else
        //    {
        //        return View(vm);
        //    }
        //}

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Update(int id)
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            Package package = _packageDataService.GetSingle(p => p.PackageId == id);

            IEnumerable<Location> locationList = _locationDataService.GetAll();

            if (package.UserId == user.Id)
            {
                PackageUpdateViewModel vm = new PackageUpdateViewModel
                {
                    PackageId = package.PackageId,
                    PackageName = package.Name,
                    LocationName = package.Location,
                    Price = package.Price,
                    Description = package.Description,
                    Picture = package.Picture,
                    IsAvailable = package.IsAvailable,
                    //LocationList = ViewBag.LocationName = new SelectList(locationList, "LocationId", "Name"),
                    LocationId = package.LocationId,
                    UserId = package.UserId
                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("Denied", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Update(PackageUpdateViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Package package = new Package
                {
                    PackageId = vm.PackageId,
                    Name = vm.PackageName,
                    Location = vm.LocationName,
                    //Location = vm.LocationList.SelectedValue.ToString(),
                    Price = vm.Price,
                    Description = vm.Description,
                    IsAvailable = vm.IsAvailable,
                    LocationId = vm.LocationId,
                    UserId = vm.UserId
                };

                if (file != null)
                {
                    var serverPath = Path.Combine(_environment.WebRootPath, "uploads/package");
                    Directory.CreateDirectory(Path.Combine(serverPath, User.Identity.Name));
                    string fileName = FileNameHelper.GetNameFormatted(Path.GetFileName(file.FileName));
                    using (var fileStream = new FileStream(Path.Combine(serverPath, User.Identity.Name, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    package.Picture = User.Identity.Name + "/" + fileName;
                }

                _packageDataService.Update(package);
                return RedirectToAction("Details", "Package", new { id = package.PackageId });
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            TempData["packageId"] = id.ToString();

            Package package = _packageDataService.GetSingle(p => p.PackageId == id); //where available ADD IF AVAIL

            IEnumerable<Feedback> feedbackList = _feedbackDataService.Query(f => f.PackageId == id);

            PackageDetailsViewModel vm = new PackageDetailsViewModel
            {
                Name = package.Name,
                Location = package.Location,
                Price = package.Price,
                Description = package.Description,
                Picture = package.Picture,
                IsAvailable = package.IsAvailable,
                Feedbacks = feedbackList
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Search(PackageSearchViewModel vm)
        {
            IEnumerable<Package> packageList = vm.Packages;

            if (vm != null)
            {
                if (!string.IsNullOrEmpty(vm.SearchString))
                {
                    packageList = _packageDataService.Query(p => p.Location == vm.SearchString && p.IsAvailable == true);
                    vm.Total = packageList.Count();
                    vm.Packages = packageList;
                }
                if (vm.MinPrice.HasValue)
                {
                    packageList = packageList.Where(p => p.Price >= vm.MinPrice && p.IsAvailable == true);
                    vm.Total = packageList.Count();
                    vm.Packages = packageList;
                }
                if (vm.MaxPrice.HasValue)
                {
                    packageList = packageList.Where(p => p.Price <= vm.MaxPrice && p.IsAvailable == true);
                    vm.Total = packageList.Count();
                    vm.Packages = packageList;
                }
            }
            else
            {
                ViewBag.PackageSearch = "No packages available. Please try another location or price";
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public IActionResult CreateSelect()
        {
            IEnumerable<Location> locationList = _locationDataService.GetAll();

            PackageCreateSelectViewModel vm = new PackageCreateSelectViewModel
            {
                Locations = locationList
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> UpdateSelect()
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            IEnumerable<Package> packageList = _packageDataService.Query(p => p.UserId == user.Id);

            PackageUpdateSelectViewModel vm = new PackageUpdateSelectViewModel
            {
                Packages = packageList,
                Count = packageList.Count()
            };

            return View(vm);
        }
    }
}

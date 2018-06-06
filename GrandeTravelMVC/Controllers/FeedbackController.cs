using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravelMVC.Models;
using GrandeTravelMVC.Services;
using GrandeTravelMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravelMVC.Controllers
{
    public class FeedbackController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Package> _packageDataService;
        private IDataService<Feedback> _feedbackDataService;

        public FeedbackController(UserManager<IdentityUser> userManager,
                                 IDataService<Package> packageService,
                                 IDataService<Feedback> feedbackService)
        {
            _userManagerService = userManager;
            _packageDataService = packageService;
            _feedbackDataService = feedbackService;
        }


        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Create(int id)
        {
            Package package = _packageDataService.GetSingle(p => p.PackageId == id);

            FeedbackCreateViewModel vm = new FeedbackCreateViewModel
            {
                PackageId = package.PackageId,
                Name = package.Name,
                Location = package.Location,
                Description = package.Description
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(FeedbackCreateViewModel vm)
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            Feedback feedback = new Feedback
            {
                Rating = vm.Rating,
                Description = vm.FDescription,
                PackageId = vm.PackageId,
                UserId = user.Id
            };
            _feedbackDataService.Create(feedback);

            return RedirectToAction("Index", "Home");

            //return RedirectToAction("Details","Package", new { id = vm.PackageId});
        }
    }
}

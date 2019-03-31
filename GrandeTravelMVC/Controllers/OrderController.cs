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
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GrandeTravelMVC.Controllers
{
    public class OrderController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Order> _orderDataService;
        private IDataService<Package> _packageDataService;

        public OrderController(UserManager<IdentityUser> userManager,
                                 IDataService<Package> packageService,
                                 IDataService<Order> orderService)
        {
            _userManagerService = userManager;
            _packageDataService = packageService;
            _orderDataService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            int packageId = int.Parse(TempData["packageId"].ToString());

            Package package = _packageDataService.GetSingle(p => p.PackageId == packageId);

            OrderCreateViewModel vm = new OrderCreateViewModel
            {
                PackageId = package.PackageId,
                Name = package.Name,
                Quantity = 1,
                Location = package.Location,
                Price = package.Price,
                Description = package.Description,
                Picture = package.Picture
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(OrderCreateViewModel vm)
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                Order order = new Order
                {
                    Date = DateTime.Now,
                    PackageName = vm.Name,
                    PackageId = vm.PackageId,
                    Quantity = vm.Quantity,
                    TotalPrice = vm.Quantity * vm.Price,
                    UserId = user.Id
                };
                _orderDataService.Create(order);

                //send email
                //var apiKey = ("xxxxxxx");
                //var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("someone1@email.com", "Jane Smith");
                //var subject = "Order confirmation: Grande Travel";
                //var to = new EmailAddress("someone2@email.com", "John Brown");
                //var plainTextContent = "Thanks for booking with us!";
                //var htmlContent = "<strong>Safe travels</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var bytes = System.IO.File.ReadAllBytes("GrandeTravelMVC/wwwroot/attachments/voucher.txt");
                //var file = Convert.ToBase64String(bytes);
                //msg.AddAttachment("voucher.txt", file);
                //var response = await client.SendEmailAsync(msg);

                return RedirectToAction("Details", "Order", new { id = order.OrderId });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Details(int id)
        {
            Order order = _orderDataService.GetSingle(o => o.OrderId == id);

            OrderDetailsViewModel vm = new OrderDetailsViewModel
            {
                OrderId = order.OrderId,
                PackageName = order.PackageName,
                Quantity = order.Quantity,
                TotalPrice = order.TotalPrice,
                Date = order.Date,
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DetailsPast()
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            IEnumerable<Order> orderList = _orderDataService.Query(o => o.UserId == user.Id);

            OrderDetailsPastViewModel vm = new OrderDetailsPastViewModel
            {
                Orders = orderList,
                Count = orderList.Count()
            };

            return View(vm);
        }
    }
}

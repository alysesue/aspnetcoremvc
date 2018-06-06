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

                //create email
                //var msg = new SendGridMessage();

                //msg.SetFrom(new EmailAddress("alyse.sue@gmail.com", "Grande Travel"));

                //var recipients = new List<EmailAddress>
                //{
                //    new EmailAddress("alyse.sue@genomix.co", "Alyse Sue"),
                //    new EmailAddress("riselongevity@gmail.com", "Rise Long")
                //};
                //msg.AddTos(recipients);

                //msg.SetSubject("Order Confirmation - Grande Travel");

                //msg.AddContent(MimeType.Text, "Hello World plain text!");
                //msg.AddContent(MimeType.Html, "<p>Hello World!</p>");

                //send email
                var apiKey = ("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");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("alyse.sue@gmail.com", "Alyse Sue");
                var subject = "Order confirmation: Grande Travel";
                var to = new EmailAddress("alyse.sue@gmail.com", "Alyse Sue");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var bytes = System.IO.File.ReadAllBytes("C:/Users/AS/Desktop/A/GrandeTravelMVC7/GrandeTravelMVC/wwwroot/attachments/voucher.txt");
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment("voucher.txt", file);
                var response = await client.SendEmailAsync(msg);

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

        //private static void Main()
        //{
        //    Execute().Wait();
        //}

        //static async Task Execute()
        //{
        //    var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("alyse.sue@gmail.com", "Grande Provider");
        //    var subject = "Sending with SendGrid is Fun";
        //    var to = new EmailAddress("alyse.sue@gmail.com", "Grande Customer");
        //    var plainTextContent = "and easy to do anywhere, even with C#";
        //    var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}

    }
}

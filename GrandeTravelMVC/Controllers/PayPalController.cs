using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandeTravelMVC.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeTravelMVC.Controllers
{
    public class PayPalController : Controller
    {
        public IActionResult CreatePayment()
        {
            var payment = PayPalPaymentService.CreatePayment("string", "sale");

            return Redirect(payment.GetApprovalUrl());
        }

        public IActionResult PaymentCancelled()
        {
            // TODO: Handle cancelled payment
            return RedirectToAction("Error");
        }

        public IActionResult PaymentSuccessful(string paymentId, string token, string PayerID)
        {
            // Execute Payment
            var payment = PayPalPaymentService.ExecutePayment(paymentId, PayerID);

            return View();
        }

    }
}

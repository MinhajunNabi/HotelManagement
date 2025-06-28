using System.Web.Mvc;
using HotelManagement.Context;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Payment simulation for bkash / nagad etc.
        public ActionResult Process(string method, int orderId)
        {
            var order = db.FoodOrders.Find(orderId);
            if (order == null)
                return HttpNotFound();

            ViewBag.Method = method;
            ViewBag.Message = $"✅ Payment via {method} completed successfully!";

            return View("PaymentSuccess", order);
        }

        // ✅ Card Payment GET – shows form
        [HttpGet]
        public ActionResult CardPayment(int orderId)
        {
            var order = db.FoodOrders.Find(orderId);
            if (order == null)
                return HttpNotFound();

            ViewBag.OrderId = orderId;
            return View(); // CardPayment.cshtml
        }

        // ✅ Card Payment POST – processes payment
        [HttpPost]
        public ActionResult CardPayment(int orderId, FormCollection form)
        {
            var order = db.FoodOrders.Find(orderId);
            if (order == null)
                return HttpNotFound();

            // Here you could validate card number, CVV, etc.
            ViewBag.Method = "Card";
            ViewBag.Message = "✅ Payment via Card completed successfully!";

            return View("PaymentSuccess", order);
        }
    }
}

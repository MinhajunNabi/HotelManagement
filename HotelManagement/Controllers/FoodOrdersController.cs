using System.Linq;
using System.Web.Mvc;
using HotelManagement.Context;
using HotelManagement.Models;
using System.Collections.Generic;

namespace HotelManagement.Controllers
{
    public class FoodOrdersController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() => View(db.FoodOrders.ToList());

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            string guestName = form["GuestName"];
            string quantityStr = form["Quantity"];
            string[] selectedItems = form.GetValues("FoodItems");
            int quantity = int.Parse(quantityStr);

            var prices = new Dictionary<string, decimal>
            {
                { "Burger", 150 },
                { "Pizza", 250 },
                { "Pasta", 200 },
                { "Chicken", 180 }
            };

            decimal total = 0;
            if (selectedItems != null)
            {
                foreach (string item in selectedItems)
                {
                    if (prices.ContainsKey(item))
                    {
                        total += prices[item];
                    }
                }
                total *= quantity;
            }

            var order = new FoodOrder
            {
                GuestName = guestName,
                SelectedItems = selectedItems != null ? string.Join(",", selectedItems) : "",
                Quantity = quantity,
                TotalPrice = total
            };

            db.FoodOrders.Add(order);
            db.SaveChanges();

            return RedirectToAction("OrderSummary", new { id = order.Id });
        }

        public ActionResult OrderSummary(int id)
        {
            var order = db.FoodOrders.Find(id);
            if (order == null)
                return HttpNotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessPayment(int orderId)
        {
            var order = db.FoodOrders.Find(orderId);
            if (order == null)
                return HttpNotFound();

            bool paymentSuccess = SimulatePayment(order.TotalPrice);

            if (paymentSuccess)
            {
                ViewBag.Message = "✅ Payment Successful!";
                return View("PaymentSuccess", order);
            }
            else
            {
                ViewBag.Message = "❌ Payment Failed. Please try again.";
                return View("PaymentFailed", order);
            }
        }

        private bool SimulatePayment(decimal amount)
        {
            // Simulated payment success always
            return true;
        }

        public ActionResult OrderFood()
        {
            if (Session["GuestId"] == null)
                return RedirectToAction("Login", "Guest", new { returnUrl = Url.Action("OrderFood", "FoodOrders") });

            return View();
        }
    }
}

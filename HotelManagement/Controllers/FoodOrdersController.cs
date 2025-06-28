using System.Linq;
using System.Web.Mvc;
using HotelManagement.Context;
using HotelManagement.Models;
using System.Collections.Generic;

public class FoodOrdersController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

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

        // ✅ Price Dictionary
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

    // ✅ STEP 2: Payment handler (new)
    [HttpPost]
    public ActionResult ProcessPayment(int orderId)
    {
        var order = db.FoodOrders.Find(orderId);
        if (order == null)
            return HttpNotFound();

        // Simulate payment (always succeeds for now)
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

    // ✅ Simulated API method (you can later replace this with real payment API call)
    private bool SimulatePayment(decimal amount)
    {
        // You can simulate delay or failure logic if needed
        return true;
    }
}

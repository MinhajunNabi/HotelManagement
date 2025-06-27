using System.Linq;
using System.Web.Mvc;
using HotelManagement.Context;
using HotelManagement.Models;

public class FoodOrdersController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index() => View(db.FoodOrders.ToList());

    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(FoodOrder order)
    {
        if (ModelState.IsValid)
        {
            db.FoodOrders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(order);
    }
}

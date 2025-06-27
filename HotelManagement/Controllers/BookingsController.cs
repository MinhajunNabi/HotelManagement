using System.Linq;
using System.Web.Mvc;
using HotelManagement.Context;
using HotelManagement.Models;

public class BookingsController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index() => View(db.Bookings.ToList());

    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(Booking booking)
    {
        if (ModelState.IsValid)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(booking);
    }
}

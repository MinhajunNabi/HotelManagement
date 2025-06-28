using System.Linq;
using System.Web.Mvc;
using HotelManagement.Context;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Bookings.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public ActionResult BookRoom()
        {
            if (Session["GuestId"] == null)
                return RedirectToAction("Login", "Guest", new { returnUrl = Url.Action("BookRoom", "Bookings") });

            return View();
        }
    }
}

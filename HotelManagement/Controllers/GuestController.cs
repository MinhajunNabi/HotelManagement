using HotelManagement.Context;
using HotelManagement.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class GuestController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Guest
        public ActionResult Index()
        {
            var guests = db.Guests.Include(g => g.Room).ToList();
            return View(guests);
        }

        // GET: Guest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var guest = db.Guests.Include(g => g.Room).FirstOrDefault(g => g.GuestId == id);
            if (guest == null) return HttpNotFound();

            return View(guest);
        }

        // GET: Guest/Create (Signup)
        public ActionResult Create(string returnUrl)
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Guest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GuestId,FullName,Email,Phone,RoomId,CheckInDate,CheckOutDate")] Guest guest, string returnUrl)
        {
            if (db.Rooms.Find(guest.RoomId) == null)
            {
                ModelState.AddModelError("RoomId", "The selected room does not exist.");
                ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", guest.RoomId);
                return View(guest);
            }

            if (ModelState.IsValid)
            {
                var room = db.Rooms.Find(guest.RoomId);
                guest.CheckInDate = guest.CheckInDate ?? System.DateTime.Now;
                guest.CheckOutDate = guest.CheckOutDate ?? System.DateTime.Now.AddDays(1);

                db.Guests.Add(guest);

                if (room != null) room.IsAvailable = false;

                db.SaveChanges();
                return RedirectToAction("Login", new { returnUrl = returnUrl });
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", guest.RoomId);
            ViewBag.ReturnUrl = returnUrl;
            return View(guest);
        }

        // GET: Guest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Guest guest = db.Guests.Find(id);
            if (guest == null) return HttpNotFound();

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", guest.RoomId);
            return View(guest);
        }

        // POST: Guest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GuestId,FullName,Email,Phone,RoomId,CheckInDate,CheckOutDate")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", guest.RoomId);
            return View(guest);
        }

        // GET: Guest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Guest guest = db.Guests.Find(id);
            if (guest == null) return HttpNotFound();

            return View(guest);
        }

        // POST: Guest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest guest = db.Guests.Find(id);
            db.Guests.Remove(guest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Guest/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Guest/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string phone, string returnUrl)
        {
            var guest = db.Guests.FirstOrDefault(g => g.Email == email && g.Phone == phone);
            if (guest != null)
            {
                Session["GuestId"] = guest.GuestId;
                Session["GuestName"] = guest.FullName;

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid email or phone.";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: Guest/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}

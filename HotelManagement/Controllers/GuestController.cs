using HotelManagement.Context;
using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotelManagement.Controllers
{
    public class GuestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Guest
        public ActionResult Index()
        {
            var guests = db.Guests.Include(g => g.Room).ToList();
            return View(guests);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var guest = db.Guests.Include(g => g.Room).FirstOrDefault(g => g.GuestId == id);
            if (guest == null) return HttpNotFound();

            return View(guest);
        }


        // GET: Guest/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber");
            return View();
        }


        // POST: Guest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GuestId,FullName,Email,Phone,RoomId")] Guest guest)
        {
            if (db.Rooms.Find(guest.RoomId) == null)
            {
                ModelState.AddModelError("RoomId", "The selected room does not exist.");
                ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", guest.RoomId);
                return View(guest);
            }

            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);

                // Optional: mark the room as unavailable
                var room = db.Rooms.Find(guest.RoomId);
                if (room != null)
                {
                    room.IsAvailable = false;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", guest.RoomId);
            return View(guest);
        }


        // GET: Guest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Guest guest = db.Guests.Find(id);
            if (guest == null) return HttpNotFound();

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", guest.RoomId); // Pass room list to dropdown
            return View(guest);
        }

        // POST: Guest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GuestId,FullName,Email,Phone,RoomId")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", guest.RoomId); // Re-populate dropdown
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
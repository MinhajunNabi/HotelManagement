﻿using HotelManagement.Context;
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
    public class RoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rooms
        public ActionResult Index()
        {
            return View(db.Rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Room room = db.Rooms.Find(id);
            if (room == null) return HttpNotFound();

            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,RoomNumber,Type,PricePerNight,IsAvailable")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Room room = db.Rooms.Find(id);
            if (room == null) return HttpNotFound();

            return View(room);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,RoomNumber,Type,PricePerNight,IsAvailable")] Room room)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return back to the Edit view with validation messages
                return View(room);
            }

            var existingRoom = db.Rooms.Find(room.RoomId);

            if (existingRoom == null)
            {
                return HttpNotFound();
            }

            // Update fields
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Type = room.Type;
            existingRoom.PricePerNight = room.PricePerNight;
            existingRoom.IsAvailable = room.IsAvailable;

            // Save changes to the database
            db.SaveChanges();

            // Redirect to the list of rooms
            return RedirectToAction("Index");
        }


        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Room room = db.Rooms.Find(id);
            if (room == null) return HttpNotFound();

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
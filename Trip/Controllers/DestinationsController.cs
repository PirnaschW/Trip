using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class DestinationsController : Controller
    {
        private TripContext db = new TripContext();

        // GET: Destinations
        public ActionResult Index()
        {
            var destinations = db.Destinations.Include(d => d.Region);
            return View(destinations.ToList());
        }

        // GET: Destinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            var MyDestinationDisp = new DestinationDisp();
            MyDestinationDisp.DestinationID = id.Value;
            MyDestinationDisp.CopyData(destination);
            return View(MyDestinationDisp);
        }

        // GET: Destinations/Create
        public ActionResult Create()
        {
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name");
            return View();
        }

        // POST: Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DestinationID,Name,RegionID,NatureRating,NightlifeRating,ShoppingRating")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", destination.RegionID);
            return View(destination);
        }

        // GET: Destinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", destination.RegionID);
            var MyDestinationDisp = new DestinationDisp();
            MyDestinationDisp.DestinationID = id.Value;
            MyDestinationDisp.CopyData(destination);
            return View(MyDestinationDisp);
        }

        // POST: Destinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DestinationDisp destination)
        {
            if (ModelState.IsValid)
            {
                var MyDestination = db.Destinations.Find(destination.DestinationID);
                MyDestination.CopyData(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", destination.RegionID);
            return View(destination);
        }

        // GET: Destinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Destination destination = db.Destinations.Find(id);
            db.Destinations.Remove(destination);
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

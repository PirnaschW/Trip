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
      IEnumerable<DestinationDisp> dDisp = (from d in db.Destinations
                                            join r in db.Regions on d.RegionID equals r.RegionID
                                            select new DestinationDisp
                                            {
                                              DestinationID = d.DestinationID,
                                              Name = d.Name,
                                              RegionID = d.RegionID,
                                              NatureRating = d.NatureRating,
                                              NightlifeRating = d.NightlifeRating,
                                              ShoppingRating = d.ShoppingRating,
                                              Region = r
                                            });
      return View(dDisp);
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
      var dDisp = new DestinationDisp();
      dDisp.CopyData(destination);
      dDisp.Region = (from r in db.Regions where r.RegionID == destination.RegionID select r).First();
      return View(dDisp);
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
    public ActionResult Create([Bind(Include = "DestinationID,Name,RegionID,NatureRating,NightlifeRating,ShoppingRating")] DestinationDisp dDisp)
    {
      if (ModelState.IsValid)
      {
        Destination destination = new Destination();
        destination.CopyData(dDisp);
        db.Destinations.Add(destination);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      dDisp.Region = (from r in db.Regions where r.RegionID == dDisp.RegionID select r).First();

      ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", dDisp.RegionID);
      return View(dDisp);
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
      var dDisp = new DestinationDisp();
      dDisp.CopyData(destination);
      dDisp.Region = (from r in db.Regions where r.RegionID == destination.RegionID select r).First();

      ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", destination.RegionID);
      return View(dDisp);
    }

    // POST: Destinations/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(DestinationDisp dDisp)
    {
      if (ModelState.IsValid)
      {
        Destination destination = db.Destinations.Find(dDisp.DestinationID);
        destination.CopyData(dDisp);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", dDisp.RegionID);
      return View(dDisp);
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

      var dDisp = new DestinationDisp();
      dDisp.CopyData(destination);
      dDisp.Region = (from r in db.Regions where r.RegionID == destination.RegionID select r).First();
      return View(dDisp);
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

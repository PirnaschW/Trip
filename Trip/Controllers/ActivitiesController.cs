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
  public class ActivitiesController : Controller
  {
    private TripContext db = new TripContext();

    // GET: Activities
    public ActionResult Index()
    {
      IEnumerable<ActivityDisp> aDisp = (from a in db.Activities
                                         join d1 in db.Destinations on a.Dest1ID equals d1.DestinationID
                                         join d2 in db.Destinations on a.Dest2ID equals d2.DestinationID
                                         select new ActivityDisp
                                         {
                                           ActivityID = a.ActivityID,
                                           Name = a.Name,
                                           Type = a.Type,
                                           Distance = a.Distance,
                                           Duration = a.Duration,
                                           ExperienceRating = a.ExperienceRating,
                                           TechnicalRating = a.TechnicalRating,
                                           EnduranceRating = a.EnduranceRating,
                                           ElusiveRating = a.ElusiveRating,
                                           Dest1ID = a.Dest1ID,
                                           Dest2ID = a.Dest2ID,
                                           Reversible = a.Reversible,
                                           Dest1 = d1,
                                           Dest2 = d2
                                         });
      return View(aDisp);
    }

    // GET: Activities/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Activity activity = db.Activities.Find(id);
      if (activity == null)
      {
        return HttpNotFound();
      }
      ActivityDisp aDisp = new ActivityDisp();
      aDisp.CopyData(activity);
      aDisp.Dest1 = (from d in db.Destinations where d.DestinationID == activity.Dest1ID select d).First();
      aDisp.Dest2 = (from d in db.Destinations where d.DestinationID == activity.Dest2ID select d).First();
      return View(aDisp);
    }

    // GET: Activities/Create
    public ActionResult Create()
    {
      ViewBag.Dest1ID = new SelectList(db.Destinations, "DestinationID", "Name");
      ViewBag.Dest2ID = new SelectList(db.Destinations, "DestinationID", "Name");
      return View();
    }

    // POST: Activities/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ActivityID,Name,Type,Dest1ID,Dest2ID,Distance,Duration,ExperienceRating,TechnicalRating,EnduranceRating,ElusiveRating")] ActivityDisp aDisp)
    {
      if (ModelState.IsValid)
      {
        Activity activity = new Activity();
        activity.CopyData(aDisp);
        db.Activities.Add(activity);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      aDisp.Dest1 = (from d in db.Destinations where d.DestinationID == aDisp.Dest1ID select d).First();
      aDisp.Dest2 = (from d in db.Destinations where d.DestinationID == aDisp.Dest2ID select d).First();

      ViewBag.Dest1ID = new SelectList(db.Destinations, "DestinationID", "Name", aDisp.Dest1ID);
      ViewBag.Dest2ID = new SelectList(db.Destinations, "DestinationID", "Name", aDisp.Dest2ID);
      return View(aDisp);
    }

    // GET: Activities/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      Activity activity = db.Activities.Find(id);
      if (activity == null)
      {
        return HttpNotFound();
      }
      var aDisp = new ActivityDisp();
      aDisp.CopyData(activity);
      aDisp.Dest1 = (from d in db.Destinations where d.DestinationID == activity.Dest1ID select d).First();
      aDisp.Dest2 = (from d in db.Destinations where d.DestinationID == activity.Dest2ID select d).First();

      ViewBag.Dest1ID = new SelectList(db.Destinations, "DestinationID", "Name", activity.Dest1ID);
      ViewBag.Dest2ID = new SelectList(db.Destinations, "DestinationID", "Name", activity.Dest2ID);
      return View(aDisp);
    }

    // POST: Activities/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(ActivityDisp aDisp)
    {
      if (ModelState.IsValid)
      {
        Activity activity = db.Activities.Find(aDisp.ActivityID);
        activity.CopyData(aDisp);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.Dest1ID = new SelectList(db.Destinations, "DestinationID", "Name", aDisp.Dest1ID);
      ViewBag.Dest2ID = new SelectList(db.Destinations, "DestinationID", "Name", aDisp.Dest2ID);
      return View(aDisp);
    }

    // GET: Activities/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Activity activity = db.Activities.Find(id);
      if (activity == null)
      {
        return HttpNotFound();
      }

      var aDisp = new ActivityDisp();
      aDisp.CopyData(activity);
      aDisp.Dest1 = (from d in db.Destinations where d.DestinationID == activity.Dest1ID select d).First();
      aDisp.Dest2 = (from d in db.Destinations where d.DestinationID == activity.Dest2ID select d).First();
      return View(aDisp);
    }

    // POST: Activities/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Activity activity = db.Activities.Find(id);
      db.Activities.Remove(activity);
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

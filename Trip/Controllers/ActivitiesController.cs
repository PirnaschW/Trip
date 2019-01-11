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
            var activities = db.Activities.Include(a => a.Start).Include(a => a.End);
            return View(activities.ToList());
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
            var MyActivityDisp = new ActivityDisp();
            MyActivityDisp.ActivityID = id.Value;
            MyActivityDisp.CopyData(activity);
            return View(MyActivityDisp);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityID,Name,Type,Distance,Duration,ExperienceRating,TechnicalRating,EnduranceRating,ElusiveRating")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Activity activity = db.Activities.Include(a => a.Start).Include(a => a.End).Where(a => a.ActivityID == id).Single();
            if (activity == null)
            {
                return HttpNotFound();
            }
            var activityDisp = new ActivityDisp();
            activityDisp.ActivityID = id.Value;
            activityDisp.CopyData(activity);

            var destinations = from d in db.Destinations
                          select new
                          {
                              d.DestinationID,
                              d.Name,
                              Selected = false,
                              d
                          };

            var radioStart = new List<SelectDisp>();
            var radioEnd = new List<SelectDisp>();
            foreach (var item in destinations)
            {
                bool start = false;
                if (activityDisp.Start != null && item.DestinationID == activityDisp.Start.DestinationID) start = true;
                radioStart.Add(new SelectDisp(item.DestinationID, item.Name, start, item.d));

                bool end = false;
                if (activityDisp.End != null && item.DestinationID == activityDisp.End.DestinationID) end = true;
                radioEnd.Add(new SelectDisp(item.DestinationID, item.Name, end, item.d));
            }
            activityDisp.StartActivities = radioStart;
            activityDisp.EndActivities = radioEnd;

            return View(activityDisp);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActivityDisp activity)
        {
            if (ModelState.IsValid)
            {
                var MyActivity = db.Activities.Find(activity.ActivityID);
                MyActivity.CopyData(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
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
            return View(activity);
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

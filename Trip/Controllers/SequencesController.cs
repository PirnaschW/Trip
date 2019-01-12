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
  public class SequencesController : Controller
  {
    private TripContext db = new TripContext();

    // GET: Sequences
    public ActionResult Index()
    {
      IEnumerable<SequenceDisp> sDisp = (from s in db.Sequences
                                         join r in db.Regions on s.RegionID equals r.RegionID
                                         select new SequenceDisp
                                         {
                                           SequenceID = s.SequenceID,
                                           Name = s.Name,
                                           RegionID = s.RegionID,
                                           Region = r,
                                           SelectedActivities =
                                             (from sa in db.SequencesToActivities
                                              join a in db.Activities on sa.ActivityID equals a.ActivityID
                                              join d1 in db.Destinations on a.Dest1ID equals d1.DestinationID
                                              join d2 in db.Destinations on a.Dest2ID equals d2.DestinationID
                                              where sa.SequenceID == s.SequenceID
                                              orderby sa.Seqnr
                                              select new SequenceActivity
                                              {
                                                Id = sa.ActivityID,
                                                Seqnr = sa.Seqnr,
                                                Activity = a,
                                                Reverted = sa.Reverted,
                                                Dest1 = d1,
                                                Dest2 = d2
                                              }).ToList()
                                         }).ToList();
      return View(sDisp);
    }

    // GET: Sequences/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Sequence sequence = db.Sequences.Find(id);
      if (sequence == null)
      {
        return HttpNotFound();
      }

      SequenceDisp sDisp = new SequenceDisp();
      sDisp.CopyData(sequence);
      sDisp.Region = (from r in db.Regions where r.RegionID == sequence.RegionID select r).First();
      // get list of Activities currently associated with this Sequence
      sDisp.SelectedActivities = (from sa in db.SequencesToActivities
                                  join a in db.Activities on sa.ActivityID equals a.ActivityID
                                  join d1 in db.Destinations on a.Dest1ID equals d1.DestinationID
                                  join d2 in db.Destinations on a.Dest2ID equals d2.DestinationID
                                  where sa.SequenceID == id
                                  orderby sa.Seqnr
                                  select new SequenceActivity
                                  {
                                    Seqnr = sa.Seqnr,
                                    Id = a.ActivityID,
                                    Activity = a,
                                    Reverted = sa.Reverted,
                                    Dest1 = d1,
                                    Dest2 = d2
                                  }).ToList();
      return View(sDisp);
    }

    // GET: Sequences/Create
    public ActionResult Create()
    {
      ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name");
      return View();
    }

    // POST: Sequences/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "SequenceID,Name,RegionID")] SequenceDisp sDisp)
    {
      if (ModelState.IsValid)
      {
        Sequence sequence = new Sequence();
        sequence.CopyData(sDisp);
        db.Sequences.Add(sequence);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      sDisp.Region = (from r in db.Regions where r.RegionID == sDisp.RegionID select r).First();

      ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", sDisp.RegionID);
      return View(sDisp);
    }

    // GET: Sequences/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Sequence sequence = db.Sequences.Find(id);
      if (sequence == null)
      {
        return HttpNotFound();
      }

      var sDisp = new SequenceDisp();
      sDisp.CopyData(sequence);
      sDisp.Region = (from r in db.Regions where r.RegionID == sDisp.RegionID select r).First();
      sDisp.SelectedActivities = (from sa in db.SequencesToActivities
                                  join a in db.Activities on sa.ActivityID equals a.ActivityID
                                  join d1 in db.Destinations on a.Dest1ID equals d1.DestinationID
                                  join d2 in db.Destinations on a.Dest2ID equals d2.DestinationID
                                  where sa.SequenceID == id
                                  orderby sa.Seqnr
                                  select new SequenceActivity
                                  {
                                    Seqnr = sa.Seqnr,
                                    Id = a.ActivityID,
                                    Activity = a,
                                    Reverted = sa.Reverted,
                                    Dest1 = d1,
                                    Dest2 = d2
                                  }).ToList();
      if (sDisp.SelectedActivities.Count() > 0)
      {
        SequenceActivity sa = sDisp.SelectedActivities[sDisp.SelectedActivities.Count() - 1];
        int dID = (sa.Reverted ? sa.Dest1.DestinationID : sa.Dest2.DestinationID);
        // limit Activities to those that start where the last one ended
        sDisp.AvailableActivities = (from a in db.Activities
                                     join d1 in db.Destinations on a.Dest1ID equals d1.DestinationID
                                     join d2 in db.Destinations on a.Dest2ID equals d2.DestinationID
                                     where d1.RegionID == sequence.RegionID & (a.Dest1ID == dID | a.Dest2ID == dID)
                                     orderby a.ExperienceRating descending
                                     select new SequenceActivity
                                     {
                                       Seqnr = 0,
                                       Id = a.ActivityID,
                                       Activity = a,
                                       Reverted = false,
                                       Dest1 = d1,
                                       Dest2 = d2
                                     }).ToList();
      }
      else
      {
        sDisp.AvailableActivities = (from a in db.Activities
                                     join d1 in db.Destinations on a.Dest1ID equals d1.DestinationID
                                     join d2 in db.Destinations on a.Dest2ID equals d2.DestinationID
                                     where d1.RegionID == sequence.RegionID
                                     orderby a.ExperienceRating descending
                                     select new SequenceActivity
                                     {
                                       Seqnr = 0,
                                       Id = a.ActivityID,
                                       Activity = a,
                                       Reverted = false,
                                       Dest1 = d1,
                                       Dest2 = d2
                                     }).ToList();
      }

      ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", sequence.RegionID);
      return View(sDisp);
    }

    // POST: Sequences/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(SequenceDisp sDisp)
    {
      if (ModelState.IsValid)
      {
        Sequence sequence = db.Sequences.Find(sDisp.SequenceID);
        sequence.CopyData(sDisp);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "Name", sDisp.RegionID);
      return View(sDisp);
    }

    // GET: Sequences/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Sequence sequence = db.Sequences.Find(id);
      if (sequence == null)
      {
        return HttpNotFound();
      }


      SequenceDisp sDisp = new SequenceDisp();
      sDisp.CopyData(sequence);
      sDisp.Region = (from r in db.Regions where r.RegionID == sequence.RegionID select r).First();
      // get list of Activities currently associated with this Sequence
      sDisp.SelectedActivities = (from sa in db.SequencesToActivities
                                  join a in db.Activities on sa.ActivityID equals a.ActivityID
                                  join d1 in db.Destinations on a.Dest1ID equals d1.DestinationID
                                  join d2 in db.Destinations on a.Dest2ID equals d2.DestinationID
                                  where sa.SequenceID == id
                                  orderby sa.Seqnr
                                  select new SequenceActivity
                                  {
                                    Seqnr = sa.Seqnr,
                                    Id = a.ActivityID,
                                    Activity = a,
                                    Reverted = sa.Reverted,
                                    Dest1 = d1,
                                    Dest2 = d2
                                  }).ToList();
      return View(sDisp);
    }

    // POST: Sequences/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Sequence sequence = db.Sequences.Find(id);
      db.Sequences.Remove(sequence);
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

    // POST: Sequences/Remove/5?Seqnr=7
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    public ActionResult Remove(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      int? h = Convert.ToInt32(HttpContext.Request.QueryString["Seqnr"]);
      if (h == null)
      {
        return HttpNotFound();
      }

      IEnumerable<SequenceToActivity> list = (from sa in db.SequencesToActivities where sa.SequenceID == id & sa.Seqnr >= h select sa);
      foreach (var item in list)
      {
        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
      }
      db.SaveChanges();
      return RedirectToAction("Edit", new { Id = id });
    }

    // POST: Sequences/Append/5?Activity=7
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    public ActionResult Append(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      int? h = Convert.ToInt32(HttpContext.Request.QueryString["Activity"]);
      if (h == null)
      {
        return HttpNotFound();
      }

      int seqnr = (from sa in db.SequencesToActivities
                   where sa.SequenceID == id
                   select sa.Seqnr).Count();
      bool reverse = false;
      if (seqnr > 0)
      {
        SequenceToActivity previous = (from sa in db.SequencesToActivities.Include(a => a.Activity)
                                       where sa.SequenceID == id & sa.Seqnr == seqnr
                                       select sa).First();
        Activity next = db.Activities.Find(h);
        int dID = previous.Reverted ? previous.Activity.Dest1ID : previous.Activity.Dest2ID;
        reverse = (dID != next.Dest1ID);
      }
      db.SequencesToActivities.Add(new SequenceToActivity((int)id, (int)h, seqnr + 1, reverse));
      db.SaveChanges();
      return RedirectToAction("Edit", new { Id = id });
    }

    // POST: Sequences/Append/5?Activity=7
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    public ActionResult Revert(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      int? h = Convert.ToInt32(HttpContext.Request.QueryString["Seqnr"]);
      if (h == null)
      {
        return HttpNotFound();
      }

      SequenceToActivity s2a = (from sa in db.SequencesToActivities
                                where sa.SequenceID == id & sa.Seqnr == h
                                select sa).First();
      s2a.Reverted = !s2a.Reverted;
      db.SaveChanges();
      return RedirectToAction("Edit", new { Id = id });
    }

  }
}

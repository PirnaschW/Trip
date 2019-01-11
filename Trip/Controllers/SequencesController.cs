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
            return View(db.Sequences.ToList());
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

            var Results = from a in db.Activities
                          select new
                          {
                              a.ActivityID,
                              a.Name,
                              Selected = ((from sa in db.SequencesToActivities
                                          where (sa.SequenceID == id) & (sa.ActivityID == a.ActivityID)
                                          select sa).Count() > 0),
                              a
                          };
            var MySequenceDisp = new SequenceDisp();
            MySequenceDisp.SequenceID = id.Value;
            MySequenceDisp.CopyData(sequence);

            var MyCheckBoxList = new List<SelectDisp>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new SelectDisp(item.ActivityID, item.Name, item.Selected, item.a));
            }
            MySequenceDisp.Activities = MyCheckBoxList;

            return View(MySequenceDisp);
        }

        // GET: Sequences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sequences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SequenceID,Name")] Sequence sequence)
        {
            if (ModelState.IsValid)
            {
                db.Sequences.Add(sequence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sequence);
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
            var Results = from a in db.Activities
                          select new
                          {
                              a.ActivityID,
                              a.Name,
                              Selected = ((from sa in db.SequencesToActivities
                                          where (sa.SequenceID == id) & (sa.ActivityID == a.ActivityID)
                                          select sa).Count() > 0),
                              a
                          };
            var MySequenceDisp = new SequenceDisp();
            MySequenceDisp.SequenceID = id.Value;
            MySequenceDisp.CopyData(sequence);

            var MyCheckBoxList = new List<SelectDisp>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new SelectDisp(item.ActivityID, item.Name, item.Selected, item.a));
            }
            MySequenceDisp.Activities = MyCheckBoxList;

            return View(MySequenceDisp);
        }

        // POST: Sequences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SequenceDisp sequence)
        {
            if (ModelState.IsValid)
            {
                var MySequence = db.Sequences.Find(sequence.SequenceID);
                MySequence.CopyData(sequence);
                foreach (var item in db.SequencesToActivities)
                {
                    if (item.SequenceID == sequence.SequenceID)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var item in sequence.Activities)
                {
                    if (item.Selected)
                    {
                        db.SequencesToActivities.Add(new SequenceToActivity(sequence.SequenceID, item.Id));
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sequence);
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
            return View(sequence);
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
    }
}

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
    public class VacationsController : Controller
    {
        private TripContext db = new TripContext();

        // GET: Vacations
        public ActionResult Index()
        {
            return View(db.Vacations.ToList());
        }

        // GET: Vacations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacation vacation = db.Vacations.Find(id);
            if (vacation == null)
            {
                return HttpNotFound();
            }

            var Results = from s in db.Sequences
                          select new
                          {
                              s.SequenceID,
                              s.Name,
                              Selected = ((from vs in db.VacationsToSequences
                                          where (vs.VacationID == id) & (vs.SequenceID == s.SequenceID)
                                          select vs).Count() > 0),
                              s
                          };
            var MyVacationDisp = new VacationDisp();
            MyVacationDisp.VacationID = id.Value;
            MyVacationDisp.CopyData(vacation);

            var MyCheckBoxList = new List<SelectDisp>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new SelectDisp(item.SequenceID, item.Name, item.Selected, item.s));
            }
            MyVacationDisp.Sequences = MyCheckBoxList;

            return View(MyVacationDisp);
        }

        // GET: Vacations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vacations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VacationID,Name")] Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                db.Vacations.Add(vacation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vacation);
        }

        // GET: Vacations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacation vacation = db.Vacations.Find(id);
            if (vacation == null)
            {
                return HttpNotFound();
            }
            var Results = from s in db.Sequences
                          select new
                          {
                              s.SequenceID,
                              s.Name,
                              Selected = ((from vs in db.VacationsToSequences
                                          where (vs.VacationID == id) & (vs.SequenceID == s.SequenceID)
                                          select vs).Count() > 0),
                              s
                          };
            var MyVacationDisp = new VacationDisp();
            MyVacationDisp.VacationID = id.Value;
            MyVacationDisp.CopyData(vacation);

            var MyCheckBoxList = new List<SelectDisp>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new SelectDisp(item.SequenceID, item.Name, item.Selected, item.s));
            }
            MyVacationDisp.Sequences = MyCheckBoxList;

            return View(MyVacationDisp);
        }

        // POST: Vacations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VacationDisp vacation)
        {
            if (ModelState.IsValid)
            {
                var MyVacation = db.Vacations.Find(vacation.VacationID);
                MyVacation.CopyData(vacation);
                foreach (var item in db.VacationsToSequences)
                {
                    if (item.VacationID == vacation.VacationID)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var item in vacation.Sequences)
                {
                    if (item.Selected)
                    {
                        db.VacationsToSequences.Add(new VacationToSequence(vacation.VacationID, item.Id));
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vacation);
        }

        // GET: Vacations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacation vacation = db.Vacations.Find(id);
            if (vacation == null)
            {
                return HttpNotFound();
            }
            return View(vacation);
        }

        // POST: Vacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacation vacation = db.Vacations.Find(id);
            db.Vacations.Remove(vacation);
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

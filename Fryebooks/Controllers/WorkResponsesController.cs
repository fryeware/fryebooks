using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fryebooks.Models;

namespace Fryebooks.Controllers
{
    [Authorize]
    public class WorkResponsesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkResponses
        public ActionResult Index()
        {
            var workResponses = db.WorkResponses.Include(w => w.Income).Include(w => w.WorkRequest);
            return View(workResponses.ToList());
        }

        // GET: WorkResponses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkResponse workResponse = db.WorkResponses.Find(id);
            if (workResponse == null)
            {
                return HttpNotFound();
            }
            return View(workResponse);
        }

        // GET: WorkResponses/Create
        public ActionResult Create()
        {
            ViewBag.IncomeId = new SelectList(db.Incomes, "Id", "Id");
            ViewBag.WorkRequestId = new SelectList(db.WorkRequests, "Id", "Description");
            return View();
        }

        // POST: WorkResponses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeStarted,TimeWorked,Description,WorkRequestId,Billable,IncomeId")] WorkResponse workResponse)
        {
            if (ModelState.IsValid)
            {
                db.WorkResponses.Add(workResponse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncomeId = new SelectList(db.Incomes, "Id", "Id", workResponse.IncomeId);
            ViewBag.WorkRequestId = new SelectList(db.WorkRequests, "Id", "Description", workResponse.WorkRequestId);
            return View(workResponse);
        }

        // GET: WorkResponses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkResponse workResponse = db.WorkResponses.Find(id);
            if (workResponse == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncomeId = new SelectList(db.Incomes, "Id", "Id", workResponse.IncomeId);
            ViewBag.WorkRequestId = new SelectList(db.WorkRequests, "Id", "Description", workResponse.WorkRequestId);
            return View(workResponse);
        }

        // POST: WorkResponses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeStarted,TimeWorked,Description,WorkRequestId,Billable,IncomeId")] WorkResponse workResponse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workResponse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncomeId = new SelectList(db.Incomes, "Id", "Id", workResponse.IncomeId);
            ViewBag.WorkRequestId = new SelectList(db.WorkRequests, "Id", "Description", workResponse.WorkRequestId);
            return View(workResponse);
        }

        // GET: WorkResponses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkResponse workResponse = db.WorkResponses.Find(id);
            if (workResponse == null)
            {
                return HttpNotFound();
            }
            return View(workResponse);
        }

        // POST: WorkResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkResponse workResponse = db.WorkResponses.Find(id);
            db.WorkResponses.Remove(workResponse);
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

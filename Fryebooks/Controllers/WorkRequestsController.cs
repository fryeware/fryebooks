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
    [Authorize(Roles = "Admin")]
    public class WorkRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkRequests
        public ActionResult Index()
        {
            var workRequests = db.WorkRequests.Include(w => w.Client);
            return View(workRequests.ToList());
        }

        // GET: WorkRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkRequest workRequest = db.WorkRequests.Find(id);
            if (workRequest == null)
            {
                return HttpNotFound();
            }
            return View(workRequest);
        }

        // GET: WorkRequests/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name");
            return View();
        }

        // POST: WorkRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequestDate,CompletionDate,Description,Estimate,ClientId")] WorkRequest workRequest)
        {
            if (ModelState.IsValid)
            {
                db.WorkRequests.Add(workRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", workRequest.ClientId);
            return View(workRequest);
        }

        // GET: WorkRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkRequest workRequest = db.WorkRequests.Find(id);
            if (workRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", workRequest.ClientId);
            return View(workRequest);
        }

        // POST: WorkRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequestDate,CompletionDate,Description,Estimate,ClientId")] WorkRequest workRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", workRequest.ClientId);
            return View(workRequest);
        }

        // GET: WorkRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkRequest workRequest = db.WorkRequests.Find(id);
            if (workRequest == null)
            {
                return HttpNotFound();
            }
            return View(workRequest);
        }

        // POST: WorkRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkRequest workRequest = db.WorkRequests.Find(id);
            db.WorkRequests.Remove(workRequest);
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

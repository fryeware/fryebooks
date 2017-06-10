using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fryebooks.Models;
using DataTables.Mvc;

namespace Fryebooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorkResponsesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult GetWorkItems([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IEnumerable<WorkViewModel> unsortedItems = (from u in db.WorkResponses
                                                        select new WorkViewModel
                                                        {
                                                            Id = u.Id,
                                                            TimeStarted = u.TimeStarted.ToString(),
                                                            TimeWorked = u.TimeWorked.ToString(),
                                                            Client = u.WorkRequest.Client.Name,
                                                            Description = u.Description,
                                                            Billable = u.Billable
                                                        });

            IEnumerable<WorkViewModel> vm = applySearchFilter(unsortedItems, requestModel);
            vm = applySortFilter(vm, requestModel);
            var paged = vm.Skip(requestModel.Start).Take(requestModel.Length);
            DataTablesResponse rv = new DataTablesResponse(requestModel.Draw, paged, vm.Count(), vm.Count());
            JsonResult jr = Json(new
            {
                sEcho = rv.draw,
                iTotalRecords = rv.recordsTotal,
                iTotalDisplayRecords = rv.recordsFiltered,
                aaData = paged
            }, JsonRequestBehavior.AllowGet);
            return jr;
        }

        /// <summary>
        /// returns a view model with elements that match the search term.
        ///   TODO:   Implement regex?
        /// </summary>
        /// <param name="unfilteredPlaces"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        private IEnumerable<WorkViewModel> applySearchFilter(IEnumerable<WorkViewModel> unfilteredVM, IDataTablesRequest requestModel)
        {
            string searchTerm = requestModel.Search.Value.ToLower();
            IEnumerable<WorkViewModel> filteredVM = unfilteredVM;
            if (searchTerm != "")
            {
                filteredVM = from p in unfilteredVM
                             where p.Client != null && p.Client.ToString().ToLower().Contains(searchTerm) ||
                             p.TimeStarted != null && p.TimeStarted.ToString().ToLower().Contains(searchTerm) ||
                             p.TimeWorked != null && p.TimeWorked.ToLower().Contains(searchTerm) ||
                             p.Description != null && p.Description.ToLower().Contains(searchTerm) ||
                             p.Billable.ToString() == searchTerm
                             select p;
            }

            return filteredVM;
        }

        /// <summary>
        /// Sorts result set by selected column and order.
        /// </summary>
        /// <param name="unsortedPlaces"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        private IEnumerable<WorkViewModel> applySortFilter(IEnumerable<WorkViewModel> unsortedVM, IDataTablesRequest requestModel)
        {
            IEnumerable<WorkViewModel> sortedItems = from p in unsortedVM
                                                               orderby p.TimeStarted
                                                               select p;
            Column sortingColumn = (from col in requestModel.Columns
                                    where col.IsOrdered == true
                                    select col).First();
            switch (sortingColumn.Name)
            {
                case "TimeStarted":
                    sortedItems = sortedItems.OrderBy(s => s.TimeStarted);
                    if (sortingColumn.SortDirection == Column.OrderDirection.Descendant)
                    {
                        sortedItems = sortedItems.OrderByDescending(s => s.TimeStarted);
                    }
                    break;
                case "TimeWorked":
                    sortedItems = sortedItems.OrderBy(s => s.TimeWorked);
                    if (sortingColumn.SortDirection == Column.OrderDirection.Descendant)
                    {
                        sortedItems = sortedItems.OrderByDescending(s => s.TimeWorked);
                    }
                    break;
                case "Description":
                    sortedItems = sortedItems.OrderBy(s => s.Description);
                    if (sortingColumn.SortDirection == Column.OrderDirection.Descendant)
                    {
                        sortedItems = sortedItems.OrderByDescending(s => s.Description);
                    }
                    break;
                case "Client":
                    sortedItems = sortedItems.OrderBy(s => s.Client);
                    if (sortingColumn.SortDirection == Column.OrderDirection.Descendant)
                    {
                        sortedItems = sortedItems.OrderByDescending(s => s.Client);
                    }
                    break;
                case "Billable":
                    sortedItems = sortedItems.OrderBy(s => s.Billable);
                    if (sortingColumn.SortDirection == Column.OrderDirection.Descendant)
                    {
                        sortedItems = sortedItems.OrderByDescending(s => s.Billable);
                    }
                    break;
                default:
                    break;
            }
            return sortedItems;
        }


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

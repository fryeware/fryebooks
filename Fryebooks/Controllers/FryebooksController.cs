using Fryebooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fryebooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FryebooksController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NewWork = getNewWorkItems();
            ViewBag.CompletedWork = getCompletedWork();
            ViewBag.Clients = getClientCount();
            ViewBag.Accuracy = getAccuracy().ToString();
            ViewBag.IncomeObj = getTotalIncome();
            ViewBag.ExpenseObj = getTotalExpense();
            ViewBag.ProfitObj = getProfit(ViewBag.IncomeObj.FlowTotal, ViewBag.ExpenseObj.FlowTotal);
            ViewBag.TotalJobsCompleted = getTotalJobs();
            ViewBag.Alerts = getAlertObjArray();
            //ViewBag.AlertDays = getAlertDays();
            ViewBag.ClientObjects = getClients();
            return View();
        }

        private dynamic getClients()
        {
            List<ClientChartObject> clientData = new List<ClientChartObject>();
            ApplicationDbContext db = new ApplicationDbContext();
            IQueryable<Client> allClients = from c in db.Clients
                                            where c.ImageUrl != null
                                            select c;
            foreach (Client c in allClients)
            {
                ClientChartObject cco = new ClientChartObject();
                cco.Client = c;
                cco.Days = Convert.ToInt32(DateTime.Now.Subtract(c.OnBoardDate).TotalDays);
                Location l = new Location();
                l.name = c.Name;
                string[] locArray = c.Location.Split(new char[] { ',' });
                double lat = Convert.ToDouble(locArray[0]);
                double lng = Convert.ToDouble(locArray[1]);
                l.latLng = new List<double>() { lat, lng };
                cco.Location = l;
                clientData.Add(cco);
            }
            return clientData;
        }

        private dynamic getAlertObjArray()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            IQueryable<Alert> alerts = (from a in db.Alerts
                                        where a.Enabled
                                        select a).OrderBy(r => r.DueDate).Take(5);
            List<AlertObject> alertData = new List<AlertObject>();
            foreach (Alert a in alerts)
            {
                AlertObject ao = new AlertObject();
                ao.DaysOld = Convert.ToInt32(a.DueDate.Subtract(DateTime.Now).TotalDays);
                ao.Alert = a;
                ao.ClassData =  "label label-success";
                if (ao.DaysOld < -10)
                {
                    ao.ClassData = "label label-danger";
                }
                else if (ao.DaysOld < -5)
                {
                    ao.ClassData = "label label-warning";
                }
                else if (ao.DaysOld < -3)
                {
                    ao.ClassData = "label label-info";
                }
                alertData.Add(ao);
            }
            return alertData.ToArray();
        }

        private dynamic getAlerts()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            IQueryable<Alert> alerts = (from a in db.Alerts
                                       where a.Enabled
                                       select a).OrderBy(r => r.DueDate).Take(5);
            List<string> alertData = new List<string>();
            foreach(Alert a in alerts)
            {
                alertData.Add(a.Description);
            }
            return alertData.ToArray();
        }

        private dynamic getAlertDays()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            IQueryable<Alert> alerts = (from a in db.Alerts
                                        where a.Enabled
                                        select a).OrderBy(r => r.DueDate).Take(5);
            List<int> alertData = new List<int>();
            foreach (Alert a in alerts)
            {
                alertData.Add(Convert.ToInt32(a.DueDate.Subtract(DateTime.Now).TotalDays));
            }
            return alertData.ToArray();
        }

        private dynamic getTotalJobs()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int totalJobs = (from wr in db.WorkRequests
                             where wr.CompletionDate != null
                             select wr).Count();
            return totalJobs;
        }

        private dynamic getTotalIncome()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            double totalIncome = 0.0;
            try
            {
                totalIncome = (from i in db.Incomes
                               select i.GrossPayment).Sum();
            }
            catch(Exception ex)
            { }
            CashflowObject io = new CashflowObject();
            io.FlowTotal = Convert.ToInt32(totalIncome);
            io.FlowColor = "black";
            return io;
        }

        private dynamic getTotalExpense()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            double totalExpense = (from i in db.Expenses
                                  select i.ExpenseAmount).Sum();
            CashflowObject cfo = new CashflowObject();
            cfo.FlowTotal = Convert.ToInt32(totalExpense);
            cfo.FlowColor = "black";
            return cfo;
        }

        private dynamic getProfit(int income, int expense)
        {
            CashflowObject cfo = new CashflowObject();
            cfo.FlowTotal = income - expense;
            cfo.FlowColor = "black";
            if (cfo.FlowTotal < 0) { cfo.FlowColor = "red"; }
            return cfo;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private int getNewWorkItems()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int newWork = (from w in db.WorkRequests
                      where w.CompletionDate == null
                      select w).Count();

            return newWork;
        }

        private dynamic getCompletedWork()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int doneWork = (from w in db.WorkRequests
                            where w.CompletionDate != null
                            select w).Count();

            return doneWork;
        }

        private dynamic getClientCount()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            int clientCount = (from w in db.Clients
                            select w).Count();

            return clientCount;
        }

        private string getAccuracy()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            IQueryable<WorkRequest> allWorkRequests = from w in db.WorkRequests
                                                      where w.CompletionDate != null
                                                      select w;
            double allEstimates = 0;
            TimeSpan allWork = new TimeSpan();
            foreach(WorkRequest wr in allWorkRequests)
            {
                allEstimates += wr.Estimate;
            }

            IQueryable<WorkResponse> allWorkResponses = from r in db.WorkResponses
                                                        where r.WorkRequest.CompletionDate != null
                                                        select r;
            foreach (WorkResponse wRes in allWorkResponses)
            {
                allWork = allWork.Add(wRes.TimeWorked);
            }
            double allWorkAsInt = allWork.TotalHours;
            double firstVald = (allEstimates / allWorkAsInt);
            firstVald = firstVald * 100;
            int firstVal = Convert.ToInt32(firstVald);
            string retVal = "100";
            if (firstVal > 0)
            {
                retVal = "+" + (firstVal - 100);
            }
            else if(firstVal < 0)
            {
                retVal = "-" + (firstVal);
            }

            return retVal;
        }
    }
}
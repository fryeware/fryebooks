﻿using DataTables.Mvc;
using Fryebooks.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fryebooks.Controllers
{
    public class DataServiceController : Controller
    {
        // GET: DataService
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetGData()
        {
            // g1 = os, g2=dev, g3=DB, g4=AD, g5=
            GraphObject gData = new GraphObject();
            Node n1 = new Node() { id = "Data Analytics", group=2};
            Node n2 = new Node() { id = "Windows Systems", group=1 };
            Node n3 = new Node() { id = "Cyber Security", group=2 };
            Node n4 = new Node() { id = "Process Automation", group=2 };
            Node n5 = new Node() { id = "Mobile Apps", group=4 };
            Node n6 = new Node() { id = "Cloud Services", group = 3 };
            Node n7 = new Node() { id = "n7", group = 2 };
            Node n8 = new Node() { id = "n8", group = 4 };
            Node n9 = new Node() { id = "n9", group = 3 };
            Node n10 = new Node() { id = "n10", group = 2 };
            Node n11 = new Node() { id = "n11", group = 4 };
            Node n12 = new Node() { id = "n12", group = 3 };
            gData.nodes = new List<Node>() { n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12 };
            Link l1 = new Link() { source = n1.id, target = n2.id, value = 15, distance=20 };
            Link l2 = new Link() { source = n1.id, target = n3.id, value = 10 };
            Link l3 = new Link() { source = n3.id, target = n4.id, value = 15 };
            Link l4 = new Link() { source = n3.id, target = n5.id, value = 14 };
            Link l5 = new Link() { source = n3.id, target = n6.id, value = 3, distance=10 };
            Link l6 = new Link() { source = n1.id, target = n7.id, value = 14 };
            Link l7 = new Link() { source = n1.id, target = n8.id, value = 14 };
            Link l8 = new Link() { source = n1.id, target = n9.id, value = 14 };
            Link l9 = new Link() { source = n9.id, target = n10.id, value = 14 };
            Link l10 = new Link() { source = n9.id, target = n11.id, value = 14 };
            Link l11 = new Link() { source = n9.id, target = n12.id, value = 14 };
            gData.links = new List<Link>() { l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11 };
            return Json(gData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMapData()
        {
            List<Location> clientLocs = new List<Location>();
            ApplicationDbContext db = new ApplicationDbContext();
            IQueryable<Client> allClients = from c in db.Clients
                                            where c.Location != null
                                            select c;
            foreach(Client c in allClients)
            {
                Location l = new Location();
                l.name = c.Name;
                string[] locArray = c.Location.Split(new char[] { ',' });
                double lat = Convert.ToDouble(locArray[0]);
                double lng = Convert.ToDouble(locArray[1]);
                l.latLng = new List<double>() { lat, lng };
                clientLocs.Add(l);
            }
            return Json(clientLocs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPieChartData()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            PieChartObject billable = new PieChartObject();
            billable.color = "rgba(0, 168, 0, 0.6)";
            //billable.highlight = "rgba(0, 168, 0, 0.6)";
            billable.label = "Billable";
            IQueryable<WorkResponse> billables = from w in db.WorkResponses
                                                 where w.Billable
                                                 select w;
            foreach(WorkResponse wr in billables)
            {
                billable.value += Convert.ToInt32(wr.TimeWorked.TotalHours);
            }

            PieChartObject nonbillable = new PieChartObject();
            nonbillable.color = "rgba(255, 0, 0, 0.6)";
            //nonbillable.highlight = "rgba(255, 0, 0, 0.6)";
            nonbillable.label = "NonBillable";
            IQueryable<WorkResponse> nonbillables = from w in db.WorkResponses
                                                 where  !w.Billable
                                                 select w;
            foreach (WorkResponse wr in nonbillables)
            {
                nonbillable.value += Convert.ToInt32(wr.TimeWorked.TotalHours);
            }
            List<PieChartObject> pieChart = new List<PieChartObject>() { billable, nonbillable };
            JsonResult jr = Json(pieChart.ToArray(), JsonRequestBehavior.AllowGet);
            string jsondata = JsonConvert.SerializeObject(jr);
            return jr;
        }

        public ActionResult GetChartData()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ChartObject chart = new ChartObject();
            chart.datasets = new List<ChartDataset>();
            chart.labels = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };
            IQueryable<Income> allIncome = from i in db.Incomes where i.PayDay.Year == 2017 
                                           select i;
            IQueryable<Expense> allExpense = from e in db.Expenses where e.ExpenseDate.Year == 2017
                                             select e;

            ChartDataset ds = new ChartDataset();
            ds.data = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach(Income i in allIncome)
            {
                if(i.PayDay.Month == 1)
                {
                    ds.data[0] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 2)
                {
                    ds.data[1] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 3)
                {
                    ds.data[2] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 4)
                {
                    ds.data[3] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 5)
                {
                    ds.data[4] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 6)
                {
                    ds.data[5] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 7)
                {
                    ds.data[6] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 8)
                {
                    ds.data[7] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 9)
                {
                    ds.data[8] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 10)
                {
                    ds.data[9] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 11)
                {
                    ds.data[10] += Convert.ToInt32(i.GrossPayment);
                }
                if (i.PayDay.Month == 12)
                {
                    ds.data[11] += Convert.ToInt32(i.GrossPayment);
                }
            }

            ds.label = "Income";
            ds.fillColor = "rgba(0, 168, 0, 0.6)";
            ds.pointColor = "rgba(0, 168, 0, 0.6)";
            ds.strokeColor = "rgba(0, 168, 0, 0.6)";
            ds.pointStrokeColor = "#c1c7d1";
            ds.pointHighlightFill = "#fff";
            ds.pointHighlightStroke = "rgb(220,220,220)";
            chart.datasets.Add(ds);

            ChartDataset ds2 = new ChartDataset();
            ds2.data = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (Expense e in allExpense)
            {
                if (e.ExpenseDate.Month == 1)
                {
                    ds2.data[0] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 2)
                {
                    ds2.data[1] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 3)
                {
                    ds2.data[2] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 4)
                {
                    ds2.data[3] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 5)
                {
                    ds2.data[4] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 6)
                {
                    ds2.data[5] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 7)
                {
                    ds2.data[6] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 8)
                {
                    ds2.data[7] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 9)
                {
                    ds2.data[9] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 10)
                {
                    ds2.data[9] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 11)
                {
                    ds2.data[10] += Convert.ToInt32(e.ExpenseAmount);
                }
                if (e.ExpenseDate.Month == 12)
                {
                    ds2.data[11] += Convert.ToInt32(e.ExpenseAmount);
                }
            }

            ds2.label = "Expense";
            ds2.fillColor = "rgba(255, 0, 0, 0.6)";
            ds2.strokeColor = "rgba(255, 0, 0, 0.6)";
            ds2.pointColor = "rgba(255, 0, 0, 0.6)";
            ds2.pointStrokeColor = "rgba(60,141,188,1)";

            ds2.pointHighlightFill = "#fff";
            ds2.pointHighlightStroke = "rgba(60,141,188,1)";
            chart.datasets.Add(ds2);

            return Json(chart, JsonRequestBehavior.AllowGet);

        }
    }
}
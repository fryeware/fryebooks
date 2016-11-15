using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class ChartModel
    {
    }

    public class ChartDataset
    {
        public string label { get; set; }
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string pointColor { get; set; }
        public string pointStrokeColor { get; set; }
        public string pointHighlightFill { get; set; }
        public string pointHighlightStroke { get; set; }
        public List<int> data { get; set; }
    }

    public class ChartObject
    {
        public List<string> labels { get; set; }
        public List<ChartDataset> datasets { get; set; }
    }

    public class PieChartObject
    {
        public string label { get; set; }
        public string color { get; set; }
        public string highlight { get; set; }
        public int value { get; set; }

    }

    public class ClientChartObject
    {
        public Client Client { get; set; }
        public Location Location { get; set; }
        public int Days { get; set; }
    }

    public class Location
    {
        public List<double> latLng { get; set; }
        public string name { get; set; }
    }

    public class CashflowObject
    {
        public int FlowTotal { get; set; }
        public string FlowColor { get; set; }
    }

    public class AlertObject
    {
        public int DaysOld { get; set; }
        public string ClassData { get; set; }
        public Alert Alert { get; set; }
    }
}
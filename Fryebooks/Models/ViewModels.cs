using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class ViewModels
    {

    }

    public class WorkViewModel
    {
        public int Id { get; set; }
        public string TimeStarted { get; set; }
        public string TimeWorked { get; set; }
        public string Description { get; set; }
        public string Client { get; set; }
        public bool Billable { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class WorkResponse
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime TimeStarted { get; set; }
        public TimeSpan TimeWorked { get; set; }
        public string Description { get; set; }
        public int WorkRequestId { get; set; }
        public WorkRequest WorkRequest { get; set; }
        public bool Billable { get; set; }
        public int? IncomeId { get; set; }
        public Income Income { get; set; }
    }
}
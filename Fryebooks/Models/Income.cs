using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class Income
    {
        public int Id { get; set; }
        public DateTime PayDay { get; set; }
        public double GrossPayment { get; set; }
        public virtual ICollection<WorkResponse> WorkResponses { get; set; }
    }
}
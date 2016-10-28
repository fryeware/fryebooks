using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class Client
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public DateTime OnBoardDate { get; set; }
        public virtual ICollection<WorkRequest> WorkRequests { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class WorkRequest
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Description { get; set; }
        public int Estimate { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public virtual ICollection<WorkResponse> WorkResponses { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Enabled { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }

    }
}
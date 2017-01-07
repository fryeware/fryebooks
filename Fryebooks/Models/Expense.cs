using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }
        public Double ExpenseAmount { get; set; }
        public bool Refundable { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        
    }
}
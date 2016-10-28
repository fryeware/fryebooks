using Fryebooks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fryebooks
{
    public class FryebooksContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<WorkRequest> WorkRequests { get; set; }
        public DbSet<WorkResponse> WorkResponses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Alert> Alerts { get; set; }
    }
}
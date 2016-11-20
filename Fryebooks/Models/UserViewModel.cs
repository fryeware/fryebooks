using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fryebooks.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public bool HasDownloadRole { get; set; }
        public bool HasAdminRole { get; set; }
        public string Id { get; set; }

    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fryebooks.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alert
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public System.DateTime DueDate { get; set; }
        public bool Enabled { get; set; }
        public Nullable<int> ClientId { get; set; }
    
        public virtual Client Client { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEP_Demo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Price
    {
        public int ID { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public int ProductID { get; set; }
        public int Price1 { get; set; }
        public bool Status { get; set; }
    
        public virtual Product Product { get; set; }
    }
}

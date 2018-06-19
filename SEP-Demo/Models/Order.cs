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
    
    public partial class Order
    {
        public int ID { get; set; }
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public int UserTrade { get; set; }
        public int UserOrder { get; set; }
        public System.DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int SubPrice { get; set; }
        public int Status { get; set; }
    
        public virtual OrderList OrderList { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Product Product { get; set; }
    }
}

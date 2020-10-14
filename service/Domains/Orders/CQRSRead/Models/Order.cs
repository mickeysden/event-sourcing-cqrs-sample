using System;
using System.Collections.Generic;
using EventSourcingCQRS.Domains.Core.CQRSRead.Models;

namespace EventSourcingCQRS.Domains.Orders.CQRSRead.Models
{
    public class Order : BaseReadEntity
    {
        public string customerId { get; set; }
        public DateTime orderDate { get; set; }
        public string orderStatus { get; set; }
        public IList<OrderLineItem> items { get; set; }
    }
    public class OrderLineItem
    {
        public string productId { get; set; }
        public int qty { get; set; }
        public double unitPrice { get; set; }
    }
}
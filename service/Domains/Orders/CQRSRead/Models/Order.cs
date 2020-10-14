using System;
using System.Collections.Generic;
using EventSourcingCQRS.Domains.Core.CQRSRead.Models;
using EventSourcingCQRS.Helpers;

namespace EventSourcingCQRS.Domains.Orders.CQRSRead.Models
{
    public class Order : BaseReadEntity
    {
        public Order(string id, string customerId, DateTime orderDate, string orderStatus)
        {
            this.aggregateId = id;
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.orderStatus = orderStatus;
            items = new List<OrderLineItem>();
        }

        public string customerId { get; set; }
        public DateTime orderDate { get; set; }
        public string orderStatus { get; set; }
        public IList<OrderLineItem> items { get; set; }
        public override string ToString()
        {
            var buf = new System.Text.StringBuilder();
            foreach (var i in items) buf.Append(i.ToString());
            return customerId + " " + orderDate + " " + orderStatus + " " + buf.ToString();
        }

    }
    public class OrderLineItem
    {
        public string productId { get; set; }
        public int qty { get; set; }
        public double unitPrice { get; set; }
        public override string ToString() => ModelHelper.ToStringHelper(this);
    }
}
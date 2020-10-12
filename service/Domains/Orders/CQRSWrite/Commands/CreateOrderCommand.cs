using System;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands
{
    public class CreateOrderCommand
    {
        public string customerId { get; set; }
        public DateTime orderDate { get; set; }
        public string orderStatus { get; set; }
    }
}
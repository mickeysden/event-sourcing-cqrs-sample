using System;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Models;
using EventSourcingCQRS.Domains.Orders.CQRSWrite.Models;
using EventSourcingCQRS.Helpers;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Events
{
    public class OrderCreatedEvent : DomainEventBase<OrderAggregateId>
    {
        public string customerId { get; private set; }
        public DateTime orderDate { get; private set; }
        public string orderStatus { get; private set; }
        OrderCreatedEvent()
        {
        }

        internal OrderCreatedEvent(OrderAggregateId aggregateId, string customerId, DateTime orderDate, string orderStatus) : base(aggregateId)
        {
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.orderStatus = orderStatus;
        }

        internal OrderCreatedEvent(OrderAggregateId aggregateId, long aggregateVersion, string customerId, DateTime orderDate, string orderStatus)
            : base(aggregateId, aggregateVersion)
        {
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.orderStatus = orderStatus;
        }

        public override IDomainEvent<OrderAggregateId> WithAggregate(OrderAggregateId aggregateId, long aggregateVersion)
        {
            return new OrderCreatedEvent(aggregateId, aggregateVersion, customerId, orderDate, orderStatus);
        }

        public override string ToString() => ModelHelper.ToStringHelper(this);
    }
}
using EventSourcingCQRS.Domains.Core.CQRSWrite.Models;
using EventSourcingCQRS.Domains.Orders.CQRSWrite.Models;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Events
{
    public class AddOrderLineItemEvent : DomainEventBase<OrderAggregateId>
    {
        public string productId { get; private set; }
        public int qty { get; private set; }
        public double unitPrice { get; private set; }
        AddOrderLineItemEvent()
        {
        }

        internal AddOrderLineItemEvent(string productId, int qty, double unitPrice) : base()
        {
            this.productId = productId;
            this.qty = qty;
            this.unitPrice = unitPrice;
        }

        internal AddOrderLineItemEvent(OrderAggregateId aggegateId, long aggregateVersion, string productId, int qty, double unitPrice)
            : base(aggegateId, aggregateVersion)
        {
            this.productId = productId;
            this.qty = qty;
            this.unitPrice = unitPrice;
        }

        public override IDomainEvent<OrderAggregateId> WithAggregate(OrderAggregateId aggregateId, long aggregateVersion)
        {
            return new AddOrderLineItemEvent(aggregateId, aggregateVersion, productId, qty, unitPrice);
        }
    }
}
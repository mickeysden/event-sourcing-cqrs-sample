using System;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Models;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Models
{
    public class OrderAggregateId : IAggregateId
    {
        public OrderAggregateId()
        {
            id = Guid.NewGuid();
        }

        public OrderAggregateId(string aggregateIdString)
        {
            id = Guid.Parse(aggregateIdString);
        }
        public Guid id { get; private set; }
        public string IdAsString() => id.ToString();
        public override string ToString() => IdAsString();
    }
}
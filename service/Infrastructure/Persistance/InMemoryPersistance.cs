using System;
using System.Collections.Generic;
using System.Text;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Models;

namespace EventSourcingCQRS.Infrastructure.Persistance
{
    public class InMemoryWritePersistance<TAggregateId>
    {
        public static IList<IDomainEvent<TAggregateId>> domainEvents = new List<IDomainEvent<TAggregateId>>();

    }

    public class InMemoryReadPersistance
    {
        public static IList<MemoryPersistanceDomainEventModel> publishedEvents = new List<MemoryPersistanceDomainEventModel>();
        public static IList<Domains.Orders.CQRSRead.Models.Order> readOrders = new List<Domains.Orders.CQRSRead.Models.Order>();
    }
}
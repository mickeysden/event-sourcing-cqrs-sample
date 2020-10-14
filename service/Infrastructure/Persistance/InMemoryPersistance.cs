using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Infrastructure.Persistance
{
    public class InMemoryPersistance
    {
        public static IList<MemoryPersistanceEventModel> domainEvents = new List<MemoryPersistanceEventModel>();
        public static IList<MemoryPersistanceDomainEventModel> publishedEvents = new List<MemoryPersistanceDomainEventModel>();
        public static IList<Domains.Orders.CQRSRead.Models.Order> readOrders = new List<Domains.Orders.CQRSRead.Models.Order>();
    }
}
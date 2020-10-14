using System;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSRead.Models;
using EventSourcingCQRS.Domains.Core.CQRSRead.Repositories;
using EventSourcingCQRS.Helpers;

namespace EventSourcingCQRS.Infrastructure.Persistance
{
    public class InMemoryReadRepository : IReadRepository
    {
        public async Task UpdateReadModelOnEventPublish(string eventType)
        {
            await Task.Delay(1);
            var latestPublishedEvent = InMemoryPersistance.publishedEvents.OrderByDescending(x => x.eventDate).FirstOrDefault();
            switch (latestPublishedEvent.eventType)
            {
                case "OrderCreatedEvent":
                    if (InMemoryPersistance.readOrders.Where(x => x.aggregateId == latestPublishedEvent.aggregateId.ToString()).FirstOrDefault() == null)
                        InMemoryPersistance.readOrders.Add(new Domains.Orders.CQRSRead.Models.Order()
                        {
                            aggregateId = latestPublishedEvent.aggregateId.ToString(),
                            customerId = latestPublishedEvent.eventData["customerId"].ToString(),
                            orderDate = (DateTime)latestPublishedEvent.eventData["orderDate"],
                            orderStatus = latestPublishedEvent.eventData["orderStatus"].ToString()
                        });
                    break;
                default: SimpleLogger.Log("Not tracking this event"); break;
            }
        }
    }
}
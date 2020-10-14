using System;
using System.Collections.Generic;
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
            SimpleLogger.Log("updates for " + eventType);
            var latestPublishedEvent = InMemoryReadPersistance.publishedEvents.OrderByDescending(x => x.eventDate).FirstOrDefault();
            switch (latestPublishedEvent.eventType)
            {
                case "OrderCreatedEvent":
                    if (InMemoryReadPersistance.readOrders.Where(x => x.aggregateId == latestPublishedEvent.aggregateId.ToString()).FirstOrDefault() == null)
                        InMemoryReadPersistance.readOrders.Add(new Domains.Orders.CQRSRead.Models.Order(
                            latestPublishedEvent.aggregateId.ToString(),
                            latestPublishedEvent.eventData["customerId"].ToString(),
                            (DateTime)latestPublishedEvent.eventData["orderDate"],
                            latestPublishedEvent.eventData["orderStatus"].ToString()));
                    break;
                case "AddOrderLineItemEvent":
                    {
                        var order = InMemoryReadPersistance.readOrders.Where(x => x.aggregateId == latestPublishedEvent.aggregateId).FirstOrDefault();
                        if (order != null)
                        {
                            var updatedOrder = CopyOrder(order);
                            updatedOrder.items.Add(new Domains.Orders.CQRSRead.Models.OrderLineItem()
                            {
                                productId = latestPublishedEvent.eventData["productId"].ToString(),
                                qty = (int)latestPublishedEvent.eventData["qty"],
                                unitPrice = (double)latestPublishedEvent.eventData["unitPrice"]
                            });
                            InMemoryReadPersistance.readOrders.Remove(order);
                            InMemoryReadPersistance.readOrders.Add(updatedOrder);
                        }
                    }
                    break;
                default: SimpleLogger.Log("Not tracking this event"); break;
            }
        }

        private static Domains.Orders.CQRSRead.Models.Order CopyOrder(Domains.Orders.CQRSRead.Models.Order order)
        {
            var updatedOrder = new Domains.Orders.CQRSRead.Models.Order(order.aggregateId, order.customerId, order.orderDate, order.orderStatus);
            if (order.items != null && order.items.Count > 0) updatedOrder.items = order.items;
            return updatedOrder;
        }
    }
}
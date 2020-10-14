using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSRead.Repositories;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;
using EventSourcingCQRS.Domains.Core.EventPublisher;
using EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands;
using EventSourcingCQRS.Domains.Orders.CQRSWrite.Models;
using EventSourcingCQRS.Helpers;
using EventSourcingCQRS.Infrastructure.Persistance;
using Serilog;
using Xunit;

namespace tests
{
    public class OrdersTests
    {
        public OrdersTests()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }
        [Fact]
        public async Task GivenOrderNotCreated_WhenCreateOrderCommand_ThenCreateANewOrderAndPublishEvent()
        {
            //Given
            var dispatcher = new CommandDispatcher();

            //When
            await dispatcher.Dispatch(new CreateOrderCommand()
            {
                customerId = "customer 1",
                orderDate = DateTime.Now,
                orderStatus = "NEW"
            });

            //Then
            Assert.True(InMemoryReadPersistance.publishedEvents[InMemoryReadPersistance.publishedEvents.Count - 1].eventType == "OrderCreatedEvent");
        }

        [Fact]
        public async Task GivenEventPublished_WhenOrderRelatedEvent_ThenUpdateOrderReadModel()
        {
            //Given
            var latestPublishedEvent = InMemoryReadPersistance.publishedEvents.OrderByDescending(x => x.eventDate).FirstOrDefault();

            //When
            if (latestPublishedEvent != null && OrderRelatedEvents().Contains(latestPublishedEvent.eventType))
            {
                //Then
                IReadRepository repo = new InMemoryReadRepository();
                await repo.UpdateReadModelOnEventPublish(latestPublishedEvent.eventType);
                Assert.True(InMemoryReadPersistance.readOrders.Where(x => x.aggregateId == latestPublishedEvent.aggregateId).FirstOrDefault() != null);
            }
        }

        [Fact]
        public async Task GivenOrderAggregate_WhenLineItemAdded_ThenUpdateAggregateAndPublishEvent()
        {
            //Given
            var publishedEvent = InMemoryReadPersistance.publishedEvents
                                    .Where(x => x.eventType == "OrderCreatedEvent")
                                    .OrderByDescending(x => x.eventDate)
                                    .FirstOrDefault();
            if (publishedEvent != null)
            {
                SimpleLogger.Log("order aggregate found " + publishedEvent.aggregateId);

                //When
                var dispatcher = new CommandDispatcher();
                await dispatcher.Dispatch(new AddOrderLineItemCommand()
                {
                    orderAggregateId = publishedEvent.aggregateId,
                    productId = Guid.NewGuid().ToString(),
                    qty = 10,
                    unitPrice = 100.0
                });


                //Then
                Assert.True(InMemoryReadPersistance.publishedEvents.Where(x => x.eventType == "AddOrderLineItemEvent").FirstOrDefault() != null);
                var readOrder = InMemoryReadPersistance.readOrders.Where(x => x.aggregateId == publishedEvent.aggregateId).FirstOrDefault();
                Assert.True(readOrder != null);
                Assert.True(readOrder.items.Any());
            }
        }

        private static HashSet<string> OrderRelatedEvents()
        {
            var set = new HashSet<string>();
            set.Add("OrderCreatedEvent");
            set.Add("AddOrderLineItemEvent");
            return set;
        }
    }
}
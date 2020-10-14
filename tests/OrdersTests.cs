using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSRead.Repositories;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;
using EventSourcingCQRS.Domains.Core.EventPublisher;
using EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands;
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
            Assert.True(InMemoryPersistance.publishedEvents[InMemoryPersistance.publishedEvents.Count - 1].eventType == "OrderCreatedEvent");
        }

        [Fact]
        public async Task GivenEventPublished_WhenOrderRelatedEvent_ThenUpdateOrderReadModel()
        {
            //Given
            var latestPublishedEvent = InMemoryPersistance.publishedEvents.OrderByDescending(x => x.eventDate).FirstOrDefault();

            //When
            if (latestPublishedEvent != null && OrderRelatedEvents().Contains(latestPublishedEvent.eventType))
            {
                //Then
                IReadRepository repo = new InMemoryReadRepository();
                await repo.UpdateReadModelOnEventPublish(latestPublishedEvent.eventType);
                Assert.True(InMemoryPersistance.readOrders.Where(x => x.aggregateId == latestPublishedEvent.aggregateId).FirstOrDefault() != null);
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
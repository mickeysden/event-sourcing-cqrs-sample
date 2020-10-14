using System;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;
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
    }
}
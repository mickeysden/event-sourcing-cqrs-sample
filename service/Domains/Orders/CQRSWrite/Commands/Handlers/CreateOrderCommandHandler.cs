using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Repositories;
using EventSourcingCQRS.Domains.Orders.CQRSWrite.Models;
using EventSourcingCQRS.Infrastructure.Persistance;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        public async Task Handle(CreateOrderCommand command)
        {
            var order = new OrderAggregate(new OrderAggregateId(), command.customerId, command.orderDate, command.orderStatus);
            InMemoryDomainEventPublisher publisher = new InMemoryDomainEventPublisher();
            IWriteRepository<OrderAggregate, OrderAggregateId> repo = new InMemoryWriteRepository<OrderAggregate, OrderAggregateId>(publisher);
            await repo.SaveAsync(order);
            // foreach (var pe in InMemoryPersistance.publishedEvents) Helpers.SimpleLogger.Log(pe.ToString());
        }
    }
}
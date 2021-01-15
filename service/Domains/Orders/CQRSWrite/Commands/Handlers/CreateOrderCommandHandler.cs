using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Repositories;
using EventSourcingCQRS.Domains.Core.EventPublisher;
using EventSourcingCQRS.Domains.Orders.CQRSWrite.Models;
using EventSourcingCQRS.Infrastructure.Persistance;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        public async Task Handle(CreateOrderCommand command)
        {
            var aggregateId = new OrderAggregateId();
            var order = new OrderAggregate(aggregateId, command.customerId, command.orderDate, command.orderStatus);
            IDomainEventPublisher<OrderAggregateId> publisher = new InMemoryDomainEventPublisher<OrderAggregateId>();
            IWriteRepository<OrderAggregate, OrderAggregateId> repo = new InMemoryWriteRepository<OrderAggregate, OrderAggregateId>(publisher);
            await repo.SaveAsync(order);
        }
    }
}
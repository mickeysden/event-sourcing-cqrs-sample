using System;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Repositories;
using EventSourcingCQRS.Domains.Core.EventPublisher;
using EventSourcingCQRS.Domains.Orders.CQRSWrite.Models;
using EventSourcingCQRS.Helpers;
using EventSourcingCQRS.Infrastructure.Persistance;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands.Handlers
{
    public class AddOrderLineItemCommandHandler : ICommandHandler<AddOrderLineItemCommand>
    {
        public async Task Handle(AddOrderLineItemCommand command)
        {
            IDomainEventPublisher<OrderAggregateId> publisher = new InMemoryDomainEventPublisher<OrderAggregateId>();
            IWriteRepository<OrderAggregate, OrderAggregateId> repo = new InMemoryWriteRepository<OrderAggregate, OrderAggregateId>(publisher);
            var orderAggregate = await repo.GetByIdAsync(command.orderAggregateId);
            orderAggregate.AddOrderLineItem(command.productId, command.qty, command.unitPrice);
            await repo.SaveAsync(orderAggregate);
        }
    }
}
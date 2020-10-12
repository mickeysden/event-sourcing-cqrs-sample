using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        public void Handle(CreateOrderCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
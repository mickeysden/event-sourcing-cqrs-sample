using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands.Handlers
{
    public class AddOrderLineItemCommandHandler : ICommandHandler<AddOrderLineItemCommand>
    {
        public void Handle(AddOrderLineItemCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
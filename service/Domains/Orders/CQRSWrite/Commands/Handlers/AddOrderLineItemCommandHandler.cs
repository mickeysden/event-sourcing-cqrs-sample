using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers;

namespace EventSourcingCQRS.Domains.Orders.CQRSWrite.Commands.Handlers
{
    public class AddOrderLineItemCommandHandler : ICommandHandler<AddOrderLineItemCommand>
    {
        public async Task Handle(AddOrderLineItemCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
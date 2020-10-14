using System.Threading.Tasks;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task Handle(TCommand command);
    }
}
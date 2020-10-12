namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        void Handle(TCommand command);
    }
}
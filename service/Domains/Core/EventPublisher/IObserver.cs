namespace EventSourcingCQRS.Domains.Core.EventPublisher
{
    public interface IObserver
    {
        void update(string eventType);
    }
}
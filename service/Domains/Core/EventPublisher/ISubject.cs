using System.Threading.Tasks;

namespace EventSourcingCQRS.Domains.Core.EventPublisher
{
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        Task NotifyObservers();
    }
}
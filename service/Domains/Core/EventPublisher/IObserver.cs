using System.Threading.Tasks;

namespace EventSourcingCQRS.Domains.Core.EventPublisher
{
    public interface IObserver
    {
        Task update(string eventType);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Domains.Core.EventPublisher
{
    public class Subject : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private string eventType { get; set; }
        public async Task setEventPublished(string eventType)
        {
            this.eventType = eventType;
            await NotifyObservers();
        }
        public async Task NotifyObservers()
        {
            foreach (var o in observers) await o.update(eventType);
        }

        public void RegisterObserver(IObserver observer) => observers.Add(observer);

        public void RemoveObserver(IObserver observer) => observers.Remove(observer);
    }
}
using System.Collections.Generic;

namespace EventSourcingCQRS.Domains.Core.EventPublisher
{
    public class Subject : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private string eventType { get; set; }
        public void setEventPublished(string eventType)
        {
            this.eventType = eventType;
            NotifyObservers();
        }
        public void NotifyObservers()
        {
            foreach (var o in observers) o.update(eventType);
        }

        public void RegisterObserver(IObserver observer) => observers.Add(observer);

        public void RemoveObserver(IObserver observer) => observers.Remove(observer);
    }
}
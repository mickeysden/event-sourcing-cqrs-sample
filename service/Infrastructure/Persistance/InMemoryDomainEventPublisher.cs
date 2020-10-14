using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.EventPublisher;
using EventSourcingCQRS.Helpers;

namespace EventSourcingCQRS.Infrastructure.Persistance
{
    public class InMemoryDomainEventPublisher : IDomainEventPublisher
    {
        private Subject subject;
        private Observer observer;
        public InMemoryDomainEventPublisher()
        {
            subject = new Subject();
            observer = new Observer(new InMemoryReadRepository(), subject);
        }
        public async Task publishEvent(string eventType, string aggregateId, IDictionary<string, object> eventData)
        {
            await Task.Delay(1);
            InMemoryPersistance.publishedEvents.Add(new MemoryPersistanceDomainEventModel()
            {
                eventType = eventType,
                eventDate = DateTime.Now,
                aggregateId = aggregateId,
                eventData = eventData
            });
            subject.setEventPublished(eventType);
        }
    }
}
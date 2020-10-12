using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Models;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Repositories;

namespace EventSourcingCQRS.Infrastructure.Persistance
{
    public class InMemoryWriteRepository<TAggregate, TAggregateId> : IWriteRepository<TAggregate, TAggregateId> where TAggregate : IAggregateRoot<TAggregateId>
    {
        public async Task SaveAsync(TAggregate aggregate)
        {
            try
            {
                IESAggregateRoot<TAggregateId> aggregatePersistence = (IESAggregateRoot<TAggregateId>)aggregate;
                foreach (var @event in aggregatePersistence.GetUncommittedEvents())
                    await AppendEventAsAsync(@event);
                aggregatePersistence.ClearUncommittedEvents();
                //TODO publish event
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private async Task AppendEventAsAsync(IDomainEvent<TAggregateId> @event)
        {
            await Task.Delay(1);
            InMemoryPersistance.domainEvents.Add(GetMemoryPersistanceEventModel(@event));
        }

        private MemoryPersistanceEventModel GetMemoryPersistanceEventModel(IDomainEvent<TAggregateId> @event)
        {
            var memoryEvent = new MemoryPersistanceEventModel();
            memoryEvent.eventId = @event.eventId;
            memoryEvent.aggregateId = @event.aggregateId.ToString();
            memoryEvent.aggregateVersion = @event.aggregateVersion;
            memoryEvent.eventDate = @event.eventDate;
            memoryEvent.eventType = @event.GetType().Name;
            var map = new Dictionary<string, object>();
            var properties = @event.GetType().GetProperties();
            foreach (var p in properties) if (!GetIgnoreProperties().Contains(p.Name)) map.Add(p.Name, p.GetValue(@event, null));
            memoryEvent.eventMap = map;
            return memoryEvent;
        }

        private HashSet<string> GetIgnoreProperties()
        {
            var result = new HashSet<string>();
            result.Add("eventId");
            result.Add("aggregateId");
            result.Add("aggregateVersion");
            result.Add("eventDate");
            result.Add("eventType");
            return result;
        }
    }
}
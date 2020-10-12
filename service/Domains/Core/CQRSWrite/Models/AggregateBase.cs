using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Models
{
    public abstract class AggregateBase<TId> : IAggregateRoot<TId>, IESAggregateRoot<TId>
    {
        public const long NewAggregateVersion = 0;
        private long _version = NewAggregateVersion;
        private IList<IDomainEvent<TId>> _uncommittedEvents = new List<IDomainEvent<TId>>();
        public TId id { get; protected set; }

        long IESAggregateRoot<TId>.version => _version;

        public void ApplyEvent(IDomainEvent<TId> @event, long version)
        {
            if (!_uncommittedEvents.Any(x => Equals(x.eventId, @event.eventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                _version = version;
            }
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public IEnumerable<IDomainEvent<TId>> GetUncommittedEvents()
        {
            return _uncommittedEvents.AsEnumerable();
        }

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent : DomainEventBase<TId>
        {
            IDomainEvent<TId> eventWithAggregate = @event.WithAggregate(
                Equals(id, default(TId)) ? @event.aggregateId : id,
                _version);

            ((IESAggregateRoot<TId>)this).ApplyEvent(eventWithAggregate, _version + 1);
            _uncommittedEvents.Add(eventWithAggregate);
        }
    }
}
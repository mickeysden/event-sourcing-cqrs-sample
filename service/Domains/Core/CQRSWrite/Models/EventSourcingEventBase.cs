using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Models
{
    public abstract class EventSourcingEventBase<TAggregateId> : IDomainEvent<TAggregateId>, IEquatable<EventSourcingEventBase<TAggregateId>>
    {
        protected EventSourcingEventBase()
        {
            eventId = Guid.NewGuid();
            eventDate = DateTime.Now;
        }
        protected EventSourcingEventBase(TAggregateId aggregateId) : this() => this.aggregateId = aggregateId;
        protected EventSourcingEventBase(TAggregateId aggregateId, long aggregateVersion) : this(aggregateId) => this.aggregateVersion = aggregateVersion;
        public Guid eventId { get; private set; }
        public TAggregateId aggregateId { get; private set; }
        public long aggregateVersion { get; private set; }
        public DateTime eventDate { get; private set; }

        public override bool Equals(object obj) => base.Equals(obj as EventSourcingEventBase<TAggregateId>);

        public bool Equals(EventSourcingEventBase<TAggregateId> other) => other != null && eventId.Equals(other.eventId);

        public override int GetHashCode()
        {
            return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(eventId);
        }
        public abstract IDomainEvent<TAggregateId> WithAggregate(TAggregateId aggregateId, long aggregateVersion);
    }
}
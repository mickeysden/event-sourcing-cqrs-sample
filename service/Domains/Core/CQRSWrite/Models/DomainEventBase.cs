using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Models
{
    public abstract class DomainEventBase<TAggregateId> : IDomainEvent<TAggregateId>, IEquatable<DomainEventBase<TAggregateId>>
    {
        protected DomainEventBase()
        {
            eventId = Guid.NewGuid();
            eventDate = DateTime.Now;
        }
        protected DomainEventBase(TAggregateId aggregateId) : this() => this.aggregateId = aggregateId;
        protected DomainEventBase(TAggregateId aggregateId, long aggregateVersion) : this(aggregateId) => this.aggregateVersion = aggregateVersion;
        public Guid eventId { get; private set; }
        public TAggregateId aggregateId { get; private set; }
        public long aggregateVersion { get; private set; }
        public DateTime eventDate { get; private set; }

        public override bool Equals(object obj) => base.Equals(obj as DomainEventBase<TAggregateId>);

        public bool Equals(DomainEventBase<TAggregateId> other) => other != null && eventId.Equals(other.eventId);

        public override int GetHashCode()
        {
            return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(eventId);
        }
        public abstract IDomainEvent<TAggregateId> WithAggregate(TAggregateId aggregateId, long aggregateVersion);
    }
}
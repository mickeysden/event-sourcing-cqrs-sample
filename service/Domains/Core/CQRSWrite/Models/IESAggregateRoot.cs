using System.Collections.Generic;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Models
{
    public interface IESAggregateRoot<TAggregateId>
    {
        long version { get; }
        void ApplyEvent(IDomainEvent<TAggregateId> @event, long version);
        IEnumerable<IDomainEvent<TAggregateId>> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
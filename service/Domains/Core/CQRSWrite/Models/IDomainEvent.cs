using System;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Models
{
    public interface IDomainEvent<TAggregateId>
    {
        Guid eventId { get; }
        TAggregateId aggregateId { get; }
        long aggregateVersion { get; }
        DateTime eventDate { get; }
    }
}
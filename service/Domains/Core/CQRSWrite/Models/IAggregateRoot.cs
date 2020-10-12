using System;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Models
{
    public interface IAggregateRoot<TId>
    {
        TId id { get; }
    }
}
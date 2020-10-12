using System;
using System.Collections.Generic;

namespace EventSourcingCQRS.Infrastructure.Persistance
{
    public class MemoryPersistanceDomainEvent
    {
        public string eventType { get; set; }
        public string aggregateId { get; set; }
        public DateTime eventDate { get; set; }
        public IDictionary<string, object> eventData { get; set; }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Domains.Core.EventPublisher
{
    public interface IDomainEventPublisher
    {
        Task publishEvent(string eventType, string aggregateId, IDictionary<string, object> eventData);
    }
}
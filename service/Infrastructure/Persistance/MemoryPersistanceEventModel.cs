using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Infrastructure.Persistance
{
    public class MemoryPersistanceEventModel
    {
        public Guid eventId { get; set; }
        public string aggregateId { get; set; }
        public long aggregateVersion { get; set; }
        public DateTime eventDate { get; set; }
        public string eventType { get; set; }
        public IDictionary<string, object> eventMap { get; set; }
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("--------------------" + eventType + "------------------\n");
            stringBuilder.Append("{eventId : " + eventId.ToString());
            stringBuilder.Append("\naggregateId : " + aggregateId.ToString());
            stringBuilder.Append("\naggregateVersion : " + aggregateVersion);
            stringBuilder.Append("\neventDate : " + eventDate.ToString());
            foreach (var key in eventMap.Keys) stringBuilder.Append("\n" + key + " : " + (eventMap[key] == null ? "" : eventMap[key]));
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
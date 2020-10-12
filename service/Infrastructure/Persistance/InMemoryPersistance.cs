using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Infrastructure.Persistance
{
    public class InMemoryPersistance
    {
        public static IList<MemoryPersistanceEventModel> domainEvents = new List<MemoryPersistanceEventModel>();
    }
}
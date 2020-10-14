using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSWrite.Models;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Repositories
{
    public interface IWriteRepository<TAggregate, TAggregateId> where TAggregate : IAggregateRoot<TAggregateId>
    {
        Task SaveAsync(TAggregate aggregate);
        Task<TAggregate> GetByIdAsync(string id);
    }
}
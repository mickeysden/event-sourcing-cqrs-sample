using System.Threading.Tasks;

namespace EventSourcingCQRS.Domains.Core.CQRSRead.Repositories
{
    public interface IReadRepository
    {
        Task UpdateReadModelOnEventPublish(string eventType);
    }
}
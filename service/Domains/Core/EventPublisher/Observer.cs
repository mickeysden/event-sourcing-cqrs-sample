using System;
using System.Threading.Tasks;
using EventSourcingCQRS.Domains.Core.CQRSRead.Repositories;

namespace EventSourcingCQRS.Domains.Core.EventPublisher
{
    public class Observer : IObserver
    {
        private IReadRepository repository;
        public Observer(IReadRepository repo, ISubject subject)
        {
            repository = repo;
            subject.RegisterObserver(this);
        }

        public async Task update(string eventType)
        {
            await repository.UpdateReadModelOnEventPublish(eventType);
        }
    }
}
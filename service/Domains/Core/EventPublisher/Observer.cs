using System;
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

        public void update(string eventType)
        {
            repository.UpdateReadModelOnEventPublish(eventType);
        }
    }
}
namespace EventSourcingCQRS.Domains.Core.CQRSRead.Models
{
    public abstract class BaseReadEntity
    {
        public string aggregateId { get; set; }
    }
}
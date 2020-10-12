namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Models
{
    public interface IAggregateId
    {
        string IdAsString();
    }
}
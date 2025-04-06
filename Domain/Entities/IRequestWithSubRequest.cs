namespace Domain.Entities
{
    public interface IRequestWithSubRequest
    {
        string requestId { get; set; }
        string subRequestId { get; set; }
    }
}
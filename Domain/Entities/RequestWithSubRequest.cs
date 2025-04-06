namespace Domain.Entities
{
    public class RequestWithSubRequest : Entity
    {
        public RequestWithSubRequest(string requestId, string subRequestId)
        {
            this.requestId = requestId;
            this.subRequestId = subRequestId;
        }

        public string requestId { get; set; }
        public string subRequestId { get; set; }

    }
}

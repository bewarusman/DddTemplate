namespace Domain.Entities
{
    public class RequestTypeWithSubRequestType : Entity
    {
        public string RequestTypeId { get; set; }
        public string SubRequestTypeId { get; set; }

        public RequestTypeWithSubRequestType WithRequestTypeId(string requestTypeId)
        {
            RequestTypeId = requestTypeId;
            return this;
        }

        public RequestTypeWithSubRequestType WithSubRequestTypeId(string subRequestTypeId)
        {
            SubRequestTypeId = subRequestTypeId;
            return this;
        }


    }
}
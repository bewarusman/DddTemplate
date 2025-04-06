namespace Domain.Entities
{
    public class Requests : Entity
    {
        public string RequestTypeId { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }

        public RequestType requestType;

        public IList<string> subRequestTypeIds;
        public IList<SubRequestType> subRequestTypes;

        public IList<string> requestInput;
        public IList<RequestInput> requestInputs;
        public IList<RequestWithSubRequest> requestWithSubRequest;

        public Requests()
        {

        }
        public Requests(string requestTypeId, string assignedTo, string status)
        {
            RequestTypeId = requestTypeId;
            AssignedTo = assignedTo;
            Status = status;
            requestWithSubRequest = new List<RequestWithSubRequest>();
            CreatedAt = DateTime.Now;
            CreatedBy = "alan.abdullah";
        }
    }
}

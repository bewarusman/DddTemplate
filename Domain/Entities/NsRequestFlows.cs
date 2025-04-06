namespace Domain.Entities
{
    public class NsRequestFlows : Entity
    {

        public string requestId { get; set; }
        public string status { get; set; }
        public string assignedToSecurity { get; set; }
        public string assignedToArchive { get; set; }
        public string securityMessage { get; set; }
        public string archiveMessage { get; set; }
        public string securityStatus { get; set; }
        public string archiveStatus { get; set; }

        public NsRequestFlows()
        {

        }

        public NsRequestFlows(string requestId, string status, string assignedToSecurity, string assignedToArchive, string securityMessage, string archiveMessage, string securityStatus, string archiveStatus)
        {
            this.requestId = requestId;
            this.status = status;
            this.assignedToSecurity = assignedToSecurity;
            this.assignedToArchive = assignedToArchive;
            this.securityMessage = securityMessage;
            this.archiveMessage = archiveMessage;
            this.securityStatus = securityStatus;
            this.archiveStatus = archiveStatus;
        }
    }
}

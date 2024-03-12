namespace Domain.SamtContext;

public class CustomerForm
{
    public  string Id { get; set; }
    public string LostMsisdn { get; set; }
    public string CurrentStatus { get; set; }
    public string Shop { get; set; }
    public string FlowType { get; set; }
    public string Agent { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<CustomerFormFlow> Flows { get; set; } = new List<CustomerFormFlow>();

    public class CustomerFormFlow
    {
        public string FlowType { get; set; }
        public DateTime FlowDate { get; set; }
        public string User { get; set; }
        public string Remark { get; set; }
        public string Message { get; set; }
    }
}
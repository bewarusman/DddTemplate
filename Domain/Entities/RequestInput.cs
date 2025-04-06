namespace Domain.Entities
{
    public class RequestInput : Entity
    {
        public string requestId { get; set; }
        public string input { get; set; }
        public bool isHidden { get; set; }
        public bool approved { get; set; }
        public bool vip { get; set; }
        public bool golden { get; set; }
        public bool audited { get; set; }

        public RequestInput()
        {

        }

        public RequestInput(string requestId, string input, bool isHidden, bool approved, bool vip, bool golden)
        {
            this.requestId = requestId;
            this.input = input;
            this.isHidden = isHidden;
            this.approved = approved;
            this.vip = vip;
            this.audited = false;
            this.golden = golden;
        }
    }
}

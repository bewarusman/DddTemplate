namespace Domain.Entities
{
    public class RequestType : Entity
    {
        public string Name { get; set; }
        public IList<SubRequestType> SubRequestTypes { get; set; }
        public bool HasMultipleInputs { get; set; }
        public string InputType { get; set; }

        public RequestType()
        {
            SubRequestTypes = new List<SubRequestType>();
        }

        public RequestType WithName(string name)
        {
            Name = name;
            return this;
        }

        public RequestType WithHasMultipleInputs(bool hasMultipleInputs)
        {
            HasMultipleInputs = hasMultipleInputs;
            return this;
        }

        public RequestType WithInputType(string inputType)
        {
            InputType = inputType;
            return this;
        }



    }
}
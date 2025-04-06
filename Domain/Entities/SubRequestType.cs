namespace Domain.Entities
{
    public class SubRequestType : Entity
    {
        public string Name { get; set; }
        public bool IsRegistrationFormRequired { get; set; }
        public bool IsDatesRequired { get; set; }
        public bool IsForSubrequests { get; set; }
        public IList<SubRequestType> subRequests { get; set; }
        public SubRequestType WithName(string name)
        {
            Name = name;
            return this;
        }

        public SubRequestType WithIsRegistrationFormRequired(bool isRegistrationFormRequired)
        {
            IsRegistrationFormRequired = isRegistrationFormRequired;
            return this;
        }

        public SubRequestType WithIsDatesRequired(bool isDatesRequired)
        {
            IsDatesRequired = isDatesRequired;
            return this;
        }


    }
}
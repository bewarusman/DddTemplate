namespace Infrastructure.Common.Configurations;

public interface IConnectionStrings
{
    string SecurityPortal { get; set; }
}

public class ConnectionStrings : IConnectionStrings
{
    public string SecurityPortal { get; set; }
}
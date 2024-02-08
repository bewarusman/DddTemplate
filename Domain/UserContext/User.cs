namespace Domain.UserContext;

public class User : Entity
{
    public string Username { get; set; }
    public string Hash { get; set; }

    public User() { }

    public User(string username, string hash)
    {
        Username = username;
        Hash = hash;
    }

    public void UpdateHash(string hash)
    {
        Hash = hash;
    }
}
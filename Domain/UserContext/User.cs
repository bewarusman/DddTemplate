using Domain.Common.Exceptions;
using Domain.Common.Rules;

namespace Domain.UserContext;

public class User : Entity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Hash { get; set; }

    public User() { }

    public User(string username, string hash , string email)
    {
        if (string.IsNullOrEmpty(username))
            throw new NotFoundException("username should not be empty!");
        
        if (string.IsNullOrEmpty(hash))
            throw new NotFoundException("username should not be empty!");

        if (string.IsNullOrEmpty(email))
            throw new NotFoundException("Email should not be empty!");

        if (!Rule.IsValidEmail(email))
            throw new InvalidFormatException("Email is not valid!");

        Username = username;
        Email = email;
        Hash = hash ;
    }

    public void UpdateHash(string hash)
    {
        Hash = hash;
    }
}
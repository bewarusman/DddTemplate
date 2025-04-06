using Domain.Common.Exceptions;

namespace Domain.UserContext;

public class NsAccount : Entity
{
    public string Username { get; set; }
    public string Password { get; set; }

    public NsAccount() { }

    public NsAccount(string username, string password)
    {
        if (string.IsNullOrEmpty(username))
            throw new NotFoundException("username should not be empty!");

        if (string.IsNullOrEmpty(password))
            throw new NotFoundException("username should not be empty!");



        Username = username;
        Password = password;
    }


}
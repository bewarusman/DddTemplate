using Domain.Common.Exceptions;
using Domain.Common.Rules;

namespace Domain.EmployeeContext;

public class Employee : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Employee() { }


    public Employee(string firstName, string lastName, string email, string phoneNumber)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new NotFoundException("First Name should not be empty!");

        if (string.IsNullOrEmpty(lastName))
            throw new NotFoundException("Last Name should not be empty!");

        if (string.IsNullOrEmpty(email))
            throw new NotFoundException("Email should not be empty!");

        if (!Rule.IsValidEmail(email))
            throw new InvalidFormatException("Email is not valid!");

        if (string.IsNullOrEmpty(phoneNumber))
            throw new NotFoundException("Phone Number should not be empty!");

        if (!Rule.IsValidPhoneNumber(phoneNumber))
            throw new InvalidFormatException("Phone Number is not valid!");

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void Update(string firstName, string lastName, string email, string phoneNumber)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new NotFoundException("First Name should not be empty!");

        if (string.IsNullOrEmpty(lastName))
            throw new NotFoundException("Last Name should not be empty!");

        if (string.IsNullOrEmpty(email))
            throw new NotFoundException("Email should not be empty!");

        if (Rule.IsValidEmail(email))
            throw new InvalidFormatException("Email is not valid!");

        if (string.IsNullOrEmpty(phoneNumber))
            throw new NotFoundException("Phone Number should not be empty!");

        if (Rule.IsValidPhoneNumber(phoneNumber))
            throw new InvalidFormatException("Phone Number is not valid!");

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
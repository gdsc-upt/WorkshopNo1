using Microsoft.AspNetCore.Http.HttpResults;

namespace WorkshopNo1.Entities;

public class Student
{
    public string Id { get; private  set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    private Student()
    {
    }
    
    public static Student Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new Exception("Fisrt Name can't be empty");
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new Exception("Last Name can't be empty");

        return new Student
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = firstName,
            LastName = lastName
        };
    }

    public void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new Exception("First Name can't be empty");

        firstName = firstName.Replace(" ", "");

        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new Exception("Last Name can't be empty");

        LastName = lastName;
    }
    

}
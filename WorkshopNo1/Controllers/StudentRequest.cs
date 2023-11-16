namespace WorkshopNo1.Controllers;

public record StudentRequest(string FirstName, string LastName);

public class StudentRequest1
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}

public struct Student2
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}


using System.Net.Mail;
using WorkshopNo1.Entities.Faculties;
using WorkshopNo1.Entities.Subjects;
using WorkshopNo1.Repository;
using WorkshopNo1.Utils.ResultPattern;

namespace WorkshopNo1.Entities.Students;

public class Student : Entity
{
    private readonly int _subjectLimit = 2;
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; set; }
    public Faculty Faculty { get; set; }
    public List<Subject> Subjects { get; set; } = new();

    private Student()
    {
    }
    
    public static async Task<Result<Student>> CreateAsync(
        IStudentRepository repo,
        Faculty faculty,
        string firstName,
        string lastName,
        string email)
    {
        if (!await repo.IsEmilUniqe(email))
            return Result.Failure<Student>(new Error(ErrorType.BadRequest, "Email is already taken"));

        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<Student>(StudentErrors.EmptyField("FirstName"));
        
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<Student>(StudentErrors.EmptyField("FirstName"));

        return new Student
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Faculty = faculty
        };
    }

    public Result SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure(StudentErrors.EmptyField("FirstName"));

        FirstName = firstName.Trim();

        return Result.Succes();
    }

    public Result SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure(StudentErrors.EmptyField("FirstName"));

        LastName = lastName.Trim();

        return Result.Succes();
    }
    
    public async Task<Result> SetEmail(IStudentRepository repo ,string email)
    {
        if(!IsEmailValid(email))
            return Result.Failure<Student>(StudentErrors.WrongEmailFormat(email));
        
        if(await repo.IsEmilUniqe(email))
            return Result.Failure<Student>(StudentErrors.UniqeEmailError(email));
        
        Email = email;

        return Result.Succes();
    }

    public Result<Student> ChangeFaculty(Faculty faculty)
    {
        if (faculty is null)
            return Result.Failure<Student>(new Error(ErrorType.BadRequest, "Faculty can't be null"));

        // change faculty
        Faculty = faculty;
        
        // clear subjects, as this is required by the business logic, it can not be done from outside
        // see Domain Driven Design: https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design
        this.Subjects.Clear();
        
        return this;
    }
    
    public Result AddSubject(Subject subject)
    {
        if (Subjects.Any(s => s.SubjectName == subject.SubjectName))
            return Result.Failure(StudentErrors);
        throw new Exception("Student can't have the same subject twice");
        
        if (Subjects.Count >= _subjectLimit)
            throw new Exception("Student can't have more than 2 subjects");

        Subjects.Add(subject);

        return Result.Succes();
    }
    
    // can be done using regex expression
    // better move this method in it's own class
    private bool IsEmailValid(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
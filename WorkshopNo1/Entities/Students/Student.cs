using WorkshopNo1.Entities.Faculties;
using WorkshopNo1.Entities.Subjects;
using WorkshopNo1.Repository;

namespace WorkshopNo1.Entities.Students;

public class Student : Entity
{
    private int subjectLimit = 2;
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; set; }
    public Faculty Faculty { get; set; }
    public List<Subject> Subjects { get; set; }

    private Student()
    {
    }
    
    public static async Task<Student> CreateAsync(
        IStudentRepository _repo,
        Faculty faculty,
        string firstName,
        string lastName,
        string email)
    {
        if (!await _repo.IsEmilUniqe(email))
        {
            throw new Exception("this email is already used by another student");
        }

        if (string.IsNullOrWhiteSpace(firstName))
            throw new Exception("First Name can't be empty");
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new Exception("Last Name can't be empty");

        return new Student
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Faculty = faculty
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
    
    public void SetEmail(string studentRequestEmail)
    {
        Email = studentRequestEmail;
    }

    public void AddSubject(Subject subject)
    {
        if (Subjects.Any(s => s.SubjectName == subject.SubjectName))
            throw new Exception("Student can't have the same subject twice");
        
        if (Subjects.Count >= subjectLimit)
            throw new Exception("Student can't have more than 2 subjects");

        Subjects.Add(subject);
    }
}
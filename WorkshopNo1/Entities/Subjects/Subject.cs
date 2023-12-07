using WorkshopNo1.Entities.Students;

namespace WorkshopNo1.Entities.Subjects;

public class Subject : Entity
{
    public string SubjectName { get; set; }
    public List<Student> Students { get; set; } = new();
    //please
}
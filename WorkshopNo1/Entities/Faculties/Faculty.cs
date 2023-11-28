using WorkshopNo1.Entities.Students;

namespace WorkshopNo1.Entities.Faculties;

public class Faculty : Entity
{
    public string Name { get; set; }
    public List<Student> Students { get; set; } = new();

    public Faculty(string name)
    {
        Name = name;
    }
}
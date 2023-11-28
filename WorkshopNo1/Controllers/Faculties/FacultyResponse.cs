using WorkshopNo1.Controllers.Students;

namespace WorkshopNo1.Controllers.Faculties;

public class FacultyResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<StudentResponse> Sudents { get; set; }
}
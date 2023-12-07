namespace WorkshopNo1.Entities.Students;

public interface IStudentRepository
{
    Task<bool> IsEmilUniqe(string email);
    // implememnt next methods: 
    public Task<List<Student>> GetAllAsync();
    // GetByIdAsync(string id);
    // Create(Student student);
    // Delete();
    // SaveAsync();
}
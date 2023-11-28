namespace WorkshopNo1.Entities.Students;

public interface IStudentRepository
{
    Task<bool> IsEmilUniqe(string email);
    // implememnt next methods: 
    // GetAllAsync();
    // GetByIdAsync(string id);
    // Create(Student student);
    // Delete();
    // SaveAsync();
}
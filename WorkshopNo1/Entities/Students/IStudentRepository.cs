namespace WorkshopNo1.Entities.Students;

public interface IStudentRepository
{
    Task<bool> IsEmilUniqe(string email);
    public Task<Student?> GetByIdAsync(string id, bool trackChanges);
    public Task<List<Student>> GetAllAsync();
    public void Create(Student student);
    public void Update(Student student);
    public void Delete(Student student);
}
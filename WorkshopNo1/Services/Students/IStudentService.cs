using WorkshopNo1.Controllers.Students;
using WorkshopNo1.Entities.Students;
using WorkshopNo1.Utils.ResultPattern;

namespace WorkshopNo1.Services.Students;

public interface IStudentService
{
    Task<Result<IEnumerable<Student>>> GetAllAsync();
    Task<Result<Student>> GetByIdAsync(string id);
    Task<Result<Student>> CreateAsync(StudentRequest student);
    Task<Result<Student>> UpdateAsync(StudentRequestForUpdate student);
    Task<Result>DeleteAsync(string id);
}
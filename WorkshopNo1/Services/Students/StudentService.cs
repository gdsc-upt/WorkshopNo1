using Microsoft.Extensions.Options;
using WorkshopNo1.Controllers.Students;
using WorkshopNo1.Entities.Faculties;
using WorkshopNo1.Entities.Students;
using WorkshopNo1.Repository;
using WorkshopNo1.Utils;
using WorkshopNo1.Utils.ResultPattern;

namespace WorkshopNo1.Services.Students;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    
    // here should be FacultyRepository instead of AppDbContext
    private readonly  AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly EmailConfig _emailConfig;

    public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork, IOptions<EmailConfig> emailConfig, AppDbContext context)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
        _emailConfig = emailConfig.Value;
        _context = context;
    }
    

    public async Task<Result<Student>> CreateAsync(StudentRequest request)
    {
        // see https://learn.microsoft.com/en-us/ef/core/change-tracking/entity-entries#find-and-findasync
        var faculty = await _context.Faculties.FindAsync(request.FacultyId);
        
        if(faculty is null)
            // Replace with FacutlyErrors class
            return Result.Failure<Student>(new Error(ErrorType.NotFound, "Faculty not found"));

        
        var result = await Student.CreateAsync(
            _studentRepository,
            faculty, 
            request.FirstName, 
            request.LastName,
            request.Email);
        
        if(result.IsFailure)
            return Result.Failure<Student>(result.Error);
        
        
        _studentRepository.Create(result.Value);
        await _unitOfWork.SaveChangesAsync();

        return result.Value;
    }

    public async Task<Result<IEnumerable<Student>>> GetAllAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task<Result<Student>> GetByIdAsync(string id)
    {
        var student = await _studentRepository.GetByIdAsync(id, false);

        if (student is null)
            return Result.Failure<Student>(StudentErrors.NotFound(id));

        return student;
    }
    
    public async Task<Result<Student>> UpdateAsync(StudentRequestForUpdate studentRequest)
    {
        var student = await _studentRepository.GetByIdAsync(studentRequest.Id, true);
        
        if(student is null)
            return Result.Failure<Student>(StudentErrors.NotFound(studentRequest.Id));

        if (studentRequest.FirstName is not null)
        {
            var result = student.SetFirstName(studentRequest.FirstName);
            
            if(result.IsFailure)
                return Result.Failure<Student>(result.Error);
        }

        if (studentRequest.LastName is not null)
        {
            var result = student.SetLastName(studentRequest.LastName);
            
            if(result.IsFailure)
                return Result.Failure<Student>(result.Error);
        }
        
        if (studentRequest.Email is not null)
        {
            var result = await student.SetEmail(_studentRepository, studentRequest.Email);
            
            if(result.IsFailure)
                return Result.Failure<Student>(result.Error);
        }

        await _unitOfWork.SaveChangesAsync();
        return student;

    }

    public async Task<Result> DeleteAsync(string id)
    {
        var student = await _studentRepository.GetByIdAsync(id, true);
        
        if(student is null)
            return Result.Failure(StudentErrors.NotFound(id));
        
        _studentRepository.Delete(student);
        await _unitOfWork.SaveChangesAsync();
        
        return Result.Succes();
    }
}
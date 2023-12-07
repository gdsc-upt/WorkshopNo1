using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Entities.Students;

namespace WorkshopNo1.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;
    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsEmilUniqe(string email)
    {
        return !await _context.Students.AnyAsync(s => s.Email == email);
    }
    
    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students
            .Include(student => student.Faculty)
            .Include(student => student.Subjects)
            .ToListAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Entities.Students;

namespace WorkshopNo1.Repository;

public class StudentRepository : Repository<Student>, IStudentRepository
{

    public StudentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        
    }
    public Task<bool> IsEmilUniqe(string email) => 
        FindByCondition(s => s.Email == email, false)
        .AnyAsync();

    public Task<Student?> GetByIdAsync(string id, bool trackChanges) => 
        FindByCondition(s => s.Id == id, trackChanges)
        .Include(s => s.Faculty)
        .Include(s => s.Subjects)
        .FirstOrDefaultAsync();

    public async Task<List<Student>> GetAllAsync()
    {
        var query = FindAll(false)
            .Include(s => s.Faculty)
            .Include(s => s.Subjects);

        return await query.ToListAsync();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Controllers.Students;
using WorkshopNo1.Entities.Faculties;
using WorkshopNo1.Repository;
using WorkshopNo1.Services;

namespace WorkshopNo1.Controllers.Faculties;

[Route("faculties")]
[ApiController]
public class FacultyController : ControllerBase
{
    private readonly AppDbContext _context;
    //implement Http - Get(id), Put, Patch and Delete
    //change the cod to work without AppDbContext class
    
    public FacultyController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FacultyResponse>>> GetFaculty()
    {
        var faculties = await _context.Faculties
            .Include(f => f.Students)
            .ToListAsync();

        return Ok(faculties.Select(f => new FacultyResponse
        {
            Id = f.Id,
            Name = f.Name,
            Sudents = f.Students.Select(s => new StudentResponse
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                FacultyId = f.Id
            }).ToList()
        }));
    }
    
    [HttpPost]
    public async Task<ActionResult<Faculty>> CreateFaculty([FromBody] FacultyRequest request)
    {
        var faculty = new Faculty(request.Name);

        _context.Add(faculty);
        await _context.SaveChangesAsync();

        return Ok(faculty);
    }

    [HttpGet("random")]
    public ActionResult GetRandom([FromServices] IRandomService service)
    {
        return Ok(service.GetRandomInt());
    }
}
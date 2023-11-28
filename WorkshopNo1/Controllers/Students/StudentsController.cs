using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Entities.Students;
using WorkshopNo1.Repository;

namespace WorkshopNo1.Controllers.Students;

[Route("students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _dbcontext; // change the cod to work without AppDbContext class
    private readonly IStudentRepository _repo;

    public StudentsController(AppDbContext dbcontext, IStudentRepository repo)
    {
        _dbcontext = dbcontext;
        _repo = repo;
    }


    [HttpGet(Name = "GetAllStudents")]
    public async Task<ActionResult> GetStudents()
    {
        var studenti = await _dbcontext.Set<Student>().ToListAsync();
        
        return Ok(studenti);
    }

    [HttpGet( "{Id}")]
    public async Task<ActionResult> GetStudents(string Id)
    {
        var student = await _dbcontext.Students
            .Where(studenti => studenti.Id == Id)
            .OrderBy(student => student.FirstName)
            .FirstOrDefaultAsync();

        if (student is null)
            return NotFound($"student with id: {Id} was not found");
        
        return Ok(student);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateStudent([FromBody] StudentRequest studentRequest)
    {
        var faculty = await _dbcontext.Faculties
            .FirstOrDefaultAsync(f => f.Id == studentRequest.FacultyId);

        if (faculty is null)
            return NotFound($"faculty with id: {studentRequest.FacultyId} was not found");
        
        Student student = null;
        try
        {
            student = await Student.CreateAsync(
                _repo,
                faculty,
                studentRequest.FirstName,
                studentRequest.LastName, 
                studentRequest.Email);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbcontext.Add(student);
        
        await _dbcontext.SaveChangesAsync();
        
        return Ok(new StudentResponse
        {
            Id = student.Id,
            Email = student.Email,
            FirstName = student.FirstName,
            LastName = student.LastName,
            FacultyId = faculty.Id
        });
    }

    
    [HttpDelete("{Id}")]
    public async Task<ActionResult> RemoveStudent(string Id)
    {
        var student = _dbcontext.Students
            .FirstOrDefault(s => s.Id == Id);

        if (student is null)
            return NotFound($"Student with id: {Id} does not exist");

        _dbcontext.Remove(student);
        await _dbcontext.SaveChangesAsync();

        return Ok($"Student with id: {Id} was removed");
    }
    
    
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateStudentFisrtName(string Id, [FromBody] string firstName)
    {
        var student = _dbcontext.Students.FirstOrDefault(s => s.Id == Id);

        if (student is null)
            return NotFound($"Student with id: {Id} does not exist");

        try
        {
            student.SetFirstName(firstName);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        await _dbcontext.SaveChangesAsync();

        return Ok(student);
    }
    
    [HttpPut("{Id}")]
    public async Task<ActionResult> UpdateStudent(string Id, [FromBody]StudentRequest studentRequest)
    {
        var student = _dbcontext.Students.FirstOrDefault(s => s.Id == Id);

        if (student is null)
            return NotFound($"Student with id: {Id} does not exist");

        try
        {
            student.SetFirstName(studentRequest.FirstName);
            student.SetLastName(studentRequest.LastName);
            student.SetEmail(studentRequest.Email);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        await _dbcontext.SaveChangesAsync();
        return Ok(student);
    }
    

}
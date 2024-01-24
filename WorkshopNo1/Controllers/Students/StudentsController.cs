using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Entities.Students;
using WorkshopNo1.Entities.Subjects;
using WorkshopNo1.Repository;
using WorkshopNo1.Services;
using WorkshopNo1.Services.Students;
using WorkshopNo1.Utils.ResultPattern;

namespace WorkshopNo1.Controllers.Students;

[Route("students")]
[ApiController]
public class StudentsController : ControllerBase
{

    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetStudents()
    {
        Result<IEnumerable<Student>> result = await _service.GetAllAsync();
        
        return Ok(result.Value.Select(Map));
    }

    [HttpGet( "{id}")]
    public async Task<ActionResult<StudentResponse>> GetStudentById(string id)
    {
        Result<Student> result = await _service.GetByIdAsync(id);
        
        if(result.IsSucces)
            return Ok(Map(result.Value));
        
        return NotFound(result.Error.Description);
    }
    
    [HttpPost]
    public async Task<ActionResult<StudentResponse>> CreateStudent([FromBody] StudentRequest studentRequest)
    {
        Result<Student> result = await _service.CreateAsync(studentRequest);
        
        if(result.IsSucces)
            return Ok(Map(result.Value));
        
        if(result.Error.Type == ErrorType.NotFound)
            return NotFound(result.Error.Description);
        
        return BadRequest(result.Error.Description);
    }

    
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveStudent(string id)
    {
        var result = await _service.DeleteAsync(id);
        
        if(result.IsFailure)
            return NotFound(result.Error.Description);
        
        return NoContent();
    }
    
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<StudentResponse>> UpdateStudentFisrtName(string id, [FromBody] string firstName)
    {
        var studentRequest = new StudentRequestForUpdate(id, firstName, null, null);

        Result<Student> result = await _service.UpdateAsync(studentRequest);

        if (result.IsSucces)
            return Ok(Map(result.Value));
        
        if(result.Error.Type == ErrorType.NotFound)
            return NotFound(result.Error.Description);
        
        return BadRequest(result.Error.Description);
    }
    
    /* ToDo: implement AddSubject in StudentService
    [HttpPatch("addSubject/{studentId}")]
    public async Task<ActionResult<StudentResponse>> AddSubject(string studentId, 
        [FromBody] string subjectId)
    {
      
    }
    */
    
    [HttpPut("{Id}")]
    public async Task<ActionResult<StudentResponse>> UpdateStudent(string Id, [FromBody]StudentRequestForUpdate studentRequest)
    {
        var result = await _service.UpdateAsync(studentRequest);
        
        if(result.IsSucces)
            return Ok(Map(result.Value));
        
        if(result.Error.Type == ErrorType.NotFound)
            return NotFound(result.Error.Description);
        
        return BadRequest(result.Error.Description);
    }
    
    
    private StudentResponse Map(Student student)
    {
        return new StudentResponse
        {
            Id = student.Id,
            Email = student.Email,
            FirstName = student.FirstName,
            LastName = student.LastName,
            FacultyId = student.Faculty.Id
        };
    }
}
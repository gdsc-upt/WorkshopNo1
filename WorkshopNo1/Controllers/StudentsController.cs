using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopNo1.Entities;
using WorkshopNo1.Repository;

namespace WorkshopNo1.Controllers;

[Route("students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _dbcontext;

    public StudentsController(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }


    [HttpGet(Name = "GetAllStudents")]
    public ActionResult GetStudents()
    {
        var studenti = _dbcontext.Set<Student>().ToList();
        
        return Ok(studenti);
    }

    [HttpGet( "{Id}")]
    public ActionResult GetStudents(string Id)
    {
        var student = _dbcontext.Students
            .Where(studenti => studenti.Id == Id)
            .OrderBy(student => student.FirstName)
            .FirstOrDefault();

        if (student is null)
            return NotFound($"student with id: {Id} was not found");
        
        return Ok(student);
    }
    
    [HttpPost]
    public ActionResult CreateStudent([FromBody] StudentRequest studentRequest)
    {
        Student student = null;
        try
        {
            student = Student.Create(studentRequest.FirstName, studentRequest.LastName);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbcontext.Add(student);
        _dbcontext.SaveChanges();
        
        return Ok(student);
    }

    
    [HttpDelete("{Id}")]
    public ActionResult RemoveStudent(string Id)
    {
        var student = _dbcontext.Students
            .FirstOrDefault(s => s.Id == Id);

        if (student is null)
            return NotFound($"Student with id: {Id} does not exist");

        _dbcontext.Remove(student);
        _dbcontext.SaveChanges();

        return Ok($"Student with id: {Id} was removed");
    }
    
    
    [HttpPatch("{Id}")]
    public ActionResult UpdateStudentFisrtName(string Id, [FromBody] string firstName)
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

        _dbcontext.SaveChanges();

        return Ok(student);
    }
    
    [HttpPut("{Id}")]
    public ActionResult UpdateStudent(string Id, [FromBody]StudentRequest studentRequest)
    {
        var student = _dbcontext.Students.FirstOrDefault(s => s.Id == Id);

        if (student is null)
            return NotFound($"Student with id: {Id} does not exist");

        try
        {
            student.SetFirstName(studentRequest.FirstName);
            student.SetLastName(studentRequest.LastName);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbcontext.SaveChanges();
        return Ok(student);
    }
    

}
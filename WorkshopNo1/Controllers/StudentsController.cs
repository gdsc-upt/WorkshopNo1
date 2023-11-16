using Microsoft.AspNetCore.Mvc;
using WorkshopNo1.Entities;

namespace WorkshopNo1.Controllers;

[Route("students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> _list = new()
    {
        Student.Create("Percic", "Dan"),
        Student.Create("Dobre", "Andrei")
    };

    [HttpGet(Name = "GetAllStudents")]
    public ActionResult GetStudents()
    {
        return Ok(_list);
    }

    [HttpGet( "{Id}")]
    public ActionResult GetStudents(string Id)
    {
        var student = _list.FirstOrDefault(s => s.Id == Id);

        if (student is null)
            return NotFound($"Student with id: {Id} does not exist");

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
        
        
        _list.Add(student);
        return Ok(studentRequest);
    }

    [HttpDelete("{Id}")]
    public ActionResult RemoveStudent(string Id)
    {
        var student = _list.FirstOrDefault(s => s.Id == Id);

        if (student is null)
            return NotFound($"Student with id: {Id} does not exist");

        _list.Remove(student);

        return Ok($"Student with id: {Id} was removed");
    }
    
    [HttpPatch("{Id}")]
    public ActionResult UpdateStudentFisrtName(string Id, [FromBody] string firstName)
    {
        var student = _list.FirstOrDefault(s => s.Id == Id);

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

        return Ok(student);
    }
    
    [HttpPut("{Id}")]
    public ActionResult UpdateStudent(string Id, [FromBody]StudentRequest studentRequest)
    {
        var student = _list.FirstOrDefault(s => s.Id == Id);

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
        

        return Ok(student);
    }
    

}
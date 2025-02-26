using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public ActionResult<List<Student>> GetAll() => _studentService.GetAllStudents();

    [HttpGet("{studyId}")]
    public ActionResult<Student> Get(string studyId)
    {
        var student = _studentService.GetStudentByStudyId(studyId);
        return student == null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
        try
        {
            _studentService.AddStudent(student);
            return CreatedAtAction(nameof(Get), new { studyId = student.StudyId }, student);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{studyId}")]
    public ActionResult Update(string studyId, Student student)
    {
        try
        {
            _studentService.UpdateStudent(studyId, student);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{studyId}")]
    public ActionResult Delete(string studyId)
    {
        var student = _studentService.GetStudentByStudyId(studyId);
        if (student == null) return NotFound();
        _studentService.DeleteStudent(studyId);
        return NoContent();
    }

    [HttpPost("{studyId}/absence")]
    public ActionResult RegisterAbsence(string studyId, [FromBody] AbsenceRequest request)
    {
        try
        {
            _studentService.RegisterAbsence(studyId, request.Date, request.IsAbsent);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class AbsenceRequest
{
    public DateTime Date { get; set; }
    public bool IsAbsent { get; set; }
}
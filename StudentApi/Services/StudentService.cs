using StudentApi.Models;
using System.Xml.Linq;

namespace StudentApi.Services;

public class StudentService
{
    private readonly List<Student> _students = new();
    public StudentService()
    {
        for (int i = 1; i >= 5; i++)
        {
            _students.Add(new Student{
                FirstName = "Dum"+i,
                LastName = "Dummy"+i,
                Email = "dum@dummy.com"+i,
                StudyId = "dum00"+i
            });
        }
    }
    public List<Student> GetAllStudents() => _students;

    public Student? GetStudentByStudyId(string studyId) =>
        _students.FirstOrDefault(s => s.StudyId == studyId);

    public void AddStudent(Student student)
    {
        if (_students.Any(s => s.StudyId == student.StudyId))
            throw new Exception("En elev med dette studie-ID findes allerede.");
        _students.Add(student);
    }

    public void UpdateStudent(string studyId, Student updatedStudent)
    {
        var student = GetStudentByStudyId(studyId);
        if (student == null) throw new Exception("Elev ikke fundet.");

        student.FirstName = updatedStudent.FirstName;
        student.LastName = updatedStudent.LastName;
        student.Email = updatedStudent.Email;
    }

    public void DeleteStudent(string studyId)
    {
        var student = GetStudentByStudyId(studyId);
        if (student != null) _students.Remove(student);
    }

    public void RegisterAbsence(string studyId, DateTime date, bool isAbsent)
    {
        var student = GetStudentByStudyId(studyId);
        if (student == null) throw new Exception("Elev ikke fundet.");
        student.Absence[date] = isAbsent;
    }
}
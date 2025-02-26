namespace StudentApi.Models;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string StudyId { get; set; } // Fx "Kew001"
    public Dictionary<DateTime, bool> Absence { get; set; } = new(); // Fravær pr. dag
}
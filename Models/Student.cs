namespace ef_module1_fluent.Models;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Зв'язок 1:1
    public StudentProfile? Profile { get; set; }

    // Зв'язок M:N
    public List<Course> Courses { get; set; } = new();
}
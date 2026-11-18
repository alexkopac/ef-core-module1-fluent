namespace ef_module1_fluent.Models;

public class Instructor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;

    // Зв'язок 1:N
    public List<Course> Courses { get; set; } = new();
}
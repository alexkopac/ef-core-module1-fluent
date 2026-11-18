namespace ef_module1_fluent.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationHours { get; set; }
    public decimal Price { get; set; }

    // Зовнішній ключ для зв'язку 1:N
    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; } = null!;

    // Зв'язок M:N
    public List<Student> Students { get; set; } = new();
}
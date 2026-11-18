namespace ef_module1_fluent.Models;

public class StudentProfile
{
    public int Id { get; set; }
    public string CardNumber { get; set; } = string.Empty;
    public string GitHubUrl { get; set; } = string.Empty;

    // Зовнішній ключ для зв'язку 1:1
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
}
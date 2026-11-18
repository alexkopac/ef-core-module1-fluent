using System.Text;
using Microsoft.EntityFrameworkCore;
using ef_module1_fluent.Data;
using ef_module1_fluent.Models;

Console.OutputEncoding = Encoding.UTF8;

using var context = new AcademyContext();

// Додаємо тестові дані, якщо база порожня
if (!context.Students.Any())
{
    var instructor1 = new Instructor { FullName = "Андрій Кольченко", Specialization = "Бази даних та .NET" };
    var instructor2 = new Instructor { FullName = "Олена Бондаренко", Specialization = "Системне програмування C++" };

    var courseEf = new Course { Title = "EF Core та Dapper", DurationHours = 36, Price = 4500m, Instructor = instructor1 };
    var courseSql = new Course { Title = "Проектування реляційних БД", DurationHours = 24, Price = 3200m, Instructor = instructor1 };
    var courseCpp = new Course { Title = "Сучасний C++ та STL", DurationHours = 48, Price = 5100m, Instructor = instructor2 };

    var student1 = new Student
    {
        FullName = "Олександр Копач",
        Email = "oleksandr@example.com",
        Profile = new StudentProfile { CardNumber = "ST-2026-001", GitHubUrl = "https://github.com/alexkopac" },
        Courses = new List<Course> { courseEf, courseSql, courseCpp }
    };

    var student2 = new Student
    {
        FullName = "Ірина Мельник",
        Email = "iryna@example.com",
        Profile = new StudentProfile { CardNumber = "ST-2026-002", GitHubUrl = "https://github.com/irynadev" },
        Courses = new List<Course> { courseEf }
    };

    context.Students.AddRange(student1, student2);
    context.SaveChanges();
}

Console.WriteLine("=== 1. Зв'язок 1:1 (Студент -> Профіль) та M:N (Студент <-> Курси) ===");
var students = context.Students
    .Include(s => s.Profile)
    .Include(s => s.Courses)
    .ToList();

foreach (var s in students)
{
    Console.WriteLine($"Студент: {s.FullName} | Квиток: {s.Profile?.CardNumber} | GitHub: {s.Profile?.GitHubUrl}");
    Console.WriteLine($"  Записаний на курси ({s.Courses.Count}): {string.Join(", ", s.Courses.Select(c => c.Title))}");
}

Console.WriteLine("\n=== 2. Зв'язок 1:N (Викладач -> Курси) ===");
var instructors = context.Instructors
    .Include(i => i.Courses)
    .ToList();

foreach (var i in instructors)
{
    Console.WriteLine($"Викладач: {i.FullName} ({i.Specialization})");
    foreach (var c in i.Courses)
    {
        Console.WriteLine($"  - Курс: \"{c.Title}\" ({c.DurationHours} год, {c.Price} грн)");
    }
}
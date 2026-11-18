using Microsoft.EntityFrameworkCore;
using ef_module1_fluent.Models;

namespace ef_module1_fluent.Data;

public class AcademyContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentProfile> StudentProfiles { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=DESKTOP-A5B95S7;Database=OnlineAcademyFluentDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Налаштування сутності Student та зв'язку 1:1 із StudentProfile
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Students");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.FullName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(s => s.Email)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasIndex(s => s.Email).IsUnique();

            // 1:1 (Один-до-одного)
            entity.HasOne(s => s.Profile)
                  .WithOne(p => p.Student)
                  .HasForeignKey<StudentProfile>(p => p.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Налаштування сутності StudentProfile
        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.ToTable("StudentProfiles");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.CardNumber)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.HasIndex(p => p.CardNumber).IsUnique();

            entity.Property(p => p.GitHubUrl)
                  .HasMaxLength(200);
        });

        // Налаштування сутності Instructor
        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.ToTable("Instructors");
            entity.HasKey(i => i.Id);

            entity.Property(i => i.FullName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(i => i.Specialization)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // Налаштування сутності Course, зв'язку 1:N та M:N
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Courses");
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Title)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(c => c.Price)
                  .HasColumnType("decimal(10,2)");

            // 1:N (Один-до-багатьох: один викладач -> багато курсів)
            entity.HasOne(c => c.Instructor)
                  .WithMany(i => i.Courses)
                  .HasForeignKey(c => c.InstructorId)
                  .OnDelete(DeleteBehavior.Restrict);

            // M:N (Багато-до-багатьох: студенти <-> курси через проміжну таблицю)
            entity.HasMany(c => c.Students)
                  .WithMany(s => s.Courses)
                  .UsingEntity(j => j.ToTable("StudentCourses"));
        });
    }
}
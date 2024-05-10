using Microsoft.EntityFrameworkCore;
using OrganisationManagement.Model;

namespace OrganisationManagement.DataAccess
{
    public class MainDbContext: DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
        public MainDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>()
                .HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId);

            builder.Entity<Lecture>()
                .HasOne(l => l.Department)
                .WithMany(d => d.Lectures)
                .HasForeignKey(l => l.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Lecture>()
                .HasOne(l => l.Faculty)
                .WithMany(f => f.Lectures)
                .HasForeignKey(l => l.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Lecture>()
                .HasMany(l => l.Prerequisites)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "LecturePrerequisite",
                    j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"),
                    j => j.HasOne<Lecture>().WithMany().HasForeignKey("PrerequisiteId")
                );

            builder.Entity<Classroom>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Classrooms)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Classroom>()
                .HasOne(c => c.Faculty)
                .WithMany(f => f.Classrooms)
                .HasForeignKey(c => c.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            ConfigureDepartmentProperties(builder);
            ConfigureFacultyProperties(builder);
            ConfigureLectureProperties(builder);
            ConfigureClassroomProperties(builder);
        }

        private static void ConfigureDepartmentProperties(ModelBuilder builder)
        {
            builder.Entity<Department>()
                .Property(d => d.Name).IsRequired();
            builder.Entity<Department>()
                .Property(d => d.Code).IsRequired();
            builder.Entity<Department>()
                .HasIndex(d => d.Code).IsUnique();
            builder.Entity<Department>()
                .Property(d => d.Id).ValueGeneratedOnAdd();
            builder.Entity<Department>()
                .Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            builder.Entity<Department>()
                .Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }

        private static void ConfigureFacultyProperties(ModelBuilder builder)
        {
            builder.Entity<Faculty>()
                .Property(f => f.Name).IsRequired();
            builder.Entity<Faculty>()
                .Property(f => f.Code).IsRequired();
            builder.Entity<Faculty>()
                .HasIndex(d => d.Code).IsUnique();
            builder.Entity<Faculty>()
                .Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Entity<Department>()
                .Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            builder.Entity<Department>()
                .Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }

        private static void ConfigureLectureProperties(ModelBuilder builder)
        {
            builder.Entity<Lecture>()
                .Property(l => l.Name).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Code).IsRequired();
            builder.Entity<Lecture>()
                .HasIndex(d => d.Code).IsUnique();
            builder.Entity<Lecture>()
                .Property(l => l.Description).IsRequired(false);
            builder.Entity<Lecture>()
                .Property(l => l.Level).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Semester).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Credit).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.HoursInWeek).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Id).ValueGeneratedOnAdd();
            builder.Entity<Lecture>()
                .Property(l => l.CreatedAt).ValueGeneratedOnAdd();
            builder.Entity<Lecture>()
                .Property(l => l.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }
        private static void ConfigureClassroomProperties(ModelBuilder builder)
        {
            builder.Entity<Classroom>()
                .Property(c => c.Name).IsRequired();
            builder.Entity<Classroom>()
                .Property(c => c.Code).IsRequired();
            builder.Entity<Classroom>()
                .HasIndex(d => d.Code).IsUnique();
            builder.Entity<Classroom>()
                .Property(c => c.CreatedAt).ValueGeneratedOnAdd();
            builder.Entity<Classroom>()
                .Property(c => c.UpdatedAt).ValueGeneratedOnAddOrUpdate();
            builder.Entity<Classroom>()
                .Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}

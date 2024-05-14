using Microsoft.EntityFrameworkCore;
using OrganisationManagement.Model;

namespace OrganisationManagement.DataAccess
{
    public class MainDbContext: DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
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

            builder.Entity<Classroom>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Classrooms)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            ConfigureDepartmentProperties(builder);
            ConfigureFacultyProperties(builder);
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

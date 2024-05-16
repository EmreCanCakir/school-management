using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Faculty>()
                .HasMany(f => f.Departments)
                .WithOne(d => d.Faculty)
                .HasForeignKey(d => d.FacultyId)
                .IsRequired();

            builder.Entity<Faculty>()
                .Navigation(f => f.Departments)
                .AutoInclude();

            builder.Entity<Department>()
                .HasMany(d => d.Classrooms)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentId)
                .IsRequired();

            builder.Entity<Department>()
                .Navigation(d => d.Classrooms)
                .AutoInclude();

            builder.Entity<Department>()
                .Navigation(d => d.Faculty)
                .AutoInclude();

            builder.Entity<Classroom>()
                .Navigation(c => c.Department)
                .AutoInclude();

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
                .Property(d => d.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Entity<Department>()
                .Property(d => d.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
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
            builder.Entity<Faculty>()
                .Property(d => d.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Entity<Faculty>()
                .Property(d => d.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
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
                .Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Entity<Classroom>()
                .Property(c => c.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Entity<Classroom>()
                .Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.DataAccess
{
    public class MainDbContext: IdentityDbContext<User>
    {
        public DbSet<UserDetail> UserDetails { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserDetail>()
                .ToTable("user_details")
                .HasOne(ud => ud.User)
                .WithOne(u => u.UserDetail)
                .HasForeignKey<User>(ud => ud.Id);

            builder.Entity<Teacher>().ToTable("user_details")
                .HasBaseType<UserDetail>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Teacher>("Teacher");

            builder.Entity<Student>().ToTable("user_details")
                .HasBaseType<UserDetail>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Student>("Student");

            builder.Entity<Teacher>()
                .Property(t => t.Title).IsRequired(false);
            builder.Entity<Teacher>()
                .Property(t => t.Position).IsRequired(false);
            builder.Entity<Teacher>()
                .Property(t => t.EmployeeNumber).IsRequired();
            builder.Entity<Teacher>()
                .Property(t => t.HireDate).IsRequired();
            builder.Entity<Teacher>()
                .Property(t => t.Salary).IsRequired(false);
            builder.Entity<Teacher>()
                .Property(t => t.TeacherStatus).IsRequired();

            builder.Entity<Student>()
                .Property(s => s.StudentNumber).IsRequired();
            builder.Entity<Student>()
                .Property(s => s.EnrollmentDate).IsRequired();
            builder.Entity<Student>()
                .Property(s => s.DegreeProgram).IsRequired(false);
            builder.Entity<Student>()
                .Property(s => s.StudentStatus).IsRequired();
            builder.Entity<Student>()
                .Property(s => s.GPA).IsRequired(false);
            builder.Entity<Student>()
                .Property(s => s.AdvisorId).IsRequired(false);
        }
    }
}

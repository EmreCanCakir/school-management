using Microsoft.AspNetCore.Identity;
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
                .HasForeignKey<UserDetail>(ud => ud.UserId);

            builder.Entity<Lecturer>().ToTable("user_details")
                .HasBaseType<UserDetail>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Lecturer>("Lecturer");

            builder.Entity<Student>().ToTable("user_details")
                .HasBaseType<UserDetail>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Student>("Student");

            ConfigureUserDetailProperties(builder);
            SeedRoles(builder);
        }

        private static void ConfigureUserDetailProperties(ModelBuilder builder)
        {
            builder.Entity<UserDetail>()
                .Property(ud => ud.IdentityNumber).IsRequired();
            builder.Entity<UserDetail>()
                .Property(ud => ud.FirstName).IsRequired();
            builder.Entity<UserDetail>()
                .Property(ud => ud.LastName).IsRequired();
            builder.Entity<UserDetail>()
                .Property(ud => ud.DepartmentId).IsRequired();
            builder.Entity<UserDetail>()
                .Property(ud => ud.FacultyId).IsRequired();
            builder.Entity<UserDetail>()
                .Property(ud => ud.UserId).IsRequired();
            builder.Entity<UserDetail>()
                .Property(ud => ud.Id).ValueGeneratedOnAdd();
            builder.Entity<UserDetail>()
                .Property(ud => ud.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Entity<UserDetail>()
                .Property(ud => ud.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Entity<Lecturer>()
                .Property(t => t.Title).IsRequired(false);
            builder.Entity<Lecturer>()
                .Property(t => t.PositionId).IsRequired();
            builder.Entity<Lecturer>()
                .Property(t => t.EmployeeNumber).IsRequired();
            builder.Entity<Lecturer>()
                .Property(t => t.HireDate).IsRequired();
            builder.Entity<Lecturer>()
                .Property(t => t.Salary).IsRequired(false);
            builder.Entity<Lecturer>()
                .Property(t => t.LecturerStatus).IsRequired();
            builder.Entity<Lecturer>()
                .Property(t => t.Id).ValueGeneratedOnAdd();

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
                .Property(s => s.AdvisorId).IsRequired();
            builder.Entity<Student>()
                .Property(t => t.Id).ValueGeneratedOnAdd();
        }
        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    ConcurrencyStamp = "1",
                    Name = "Student",
                    NormalizedName = "STUDENT"
                },
                new IdentityRole
                {
                    ConcurrencyStamp = "2",
                    Name = "Lecturer",
                    NormalizedName = "LECTURER"
                },
                new IdentityRole
                {
                    ConcurrencyStamp = "3",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }
    }
}

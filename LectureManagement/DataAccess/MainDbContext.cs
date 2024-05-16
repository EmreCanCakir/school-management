using LectureManagement.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Emit;
using DayOfWeek = LectureManagement.Model.DayOfWeek;

namespace LectureManagement.DataAccess
{
    public class MainDbContext: DbContext
    {
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureSchedule> LectureSchedules { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<LectureInstructor> LectureInstructors { get; set; }
        public DbSet<LectureStudent> LectureStudents { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
        public MainDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Lecture>()
                .HasMany(l => l.Prerequisites)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "LecturePrerequisite",
                    j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"),
                    j => j.HasOne<Lecture>().WithMany().HasForeignKey("PrerequisiteId")
                );

            builder.Entity<LectureSchedule>()
                .HasOne(s => s.Lecture)
                .WithMany(l => l.Schedules)
                .HasForeignKey(s => s.LectureId);

            builder.Entity<LectureSchedule>()
                .HasOne(s => s.AcademicYear)
                .WithMany()
                .HasForeignKey(s => s.AcademicYearId);

            builder.Entity<LectureInstructor>()
                .HasOne(i => i.Lecture)
                .WithMany(l => l.Instructors)
                .HasForeignKey(i => i.LectureId);

            builder.Entity<LectureInstructor>()
                .HasOne(i => i.AcademicYear)
                .WithMany()
                .HasForeignKey(i => i.AcademicYearId);

            builder.Entity<LectureStudent>()
                .HasOne(s => s.Lecture)
                .WithMany(l => l.Students)
                .HasForeignKey(s => s.LectureId);

            builder.Entity<LectureStudent>()
                .HasOne(s => s.AcademicYear)
                .WithMany()
                .HasForeignKey(s => s.AcademicYearId);

            ConfigureLectureProperties(builder);
            ConfigureLectureScheduleProperties(builder);
            ConfigureAcademicYearProperties(builder);
            ConfigureLectureInstructorProperties(builder);
            ConfigureLectureStudentProperties(builder);
            SetEagerLoading(builder);
        }

        private static void SetEagerLoading(ModelBuilder builder)
        {
            builder.Entity<Lecture>()
                .Navigation(l => l.Prerequisites)
                .AutoInclude();
            builder.Entity<Lecture>()
                .Navigation(l => l.Schedules)
                .AutoInclude();
            builder.Entity<Lecture>()
                .Navigation(l => l.Instructors)
                .AutoInclude();
            builder.Entity<Lecture>()
                .Navigation(l => l.Students)
                .AutoInclude();

            builder.Entity<LectureSchedule>()
                .Navigation(s => s.Lecture)
                .AutoInclude();
            builder.Entity<LectureSchedule>()
                .Navigation(s => s.AcademicYear)
                .AutoInclude();
            
            builder.Entity<LectureInstructor>()
                .Navigation(i => i.Lecture)
                .AutoInclude();
            builder.Entity<LectureInstructor>()
                .Navigation(i => i.AcademicYear)
                .AutoInclude();

            builder.Entity<LectureStudent>()
                .Navigation(s => s.Lecture)
                .AutoInclude();
            builder.Entity<LectureStudent>()
                .Navigation(s => s.AcademicYear)
                .AutoInclude();
        }

        private static void ConfigureLectureProperties(ModelBuilder builder)
        {
            builder.Entity<Lecture>()
                .Property(l => l.Name).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Code).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Description).IsRequired(false);
            builder.Entity<Lecture>()
                .Property(l => l.Level).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Semester).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Type).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.IsGroup).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Quota).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Credit).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.HoursInWeek).IsRequired();
            builder.Entity<Lecture>()
                .Property(l => l.Id).ValueGeneratedOnAdd();
            builder.Entity<Lecture>()
                .Property(l => l.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Entity<Lecture>()
                .Property(l => l.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        private static void ConfigureLectureScheduleProperties(ModelBuilder builder)
        {
            builder.Entity<LectureSchedule>()
                .Property(s => s.LectureId).IsRequired();
            builder.Entity<LectureSchedule>()
                .Property(s => s.AcademicYearId).IsRequired();
            builder.Entity<LectureSchedule>()
                .Property(s => s.Semester).IsRequired();
            builder.Entity<LectureSchedule>()
                .Property(s => s.Schedule)
                .IsRequired()
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>>(v)
                );

            builder.Entity<LectureSchedule>()
                .Property(s => s.ClassroomId).ValueGeneratedOnAdd();
        }

        private static void ConfigureAcademicYearProperties(ModelBuilder builder)
        {
            builder.Entity<AcademicYear>()
                .Property(a => a.StartDate).IsRequired();
            builder.Entity<AcademicYear>()
                .HasIndex(a => a.StartDate).IsUnique();
            builder.Entity<AcademicYear>()
                .Property(a => a.EndDate).IsRequired();
            builder.Entity<AcademicYear>()
                .HasIndex(a => a.EndDate).IsUnique();
            builder.Entity<AcademicYear>()
                .Property(a => a.Id).ValueGeneratedOnAdd();
        }

        private static void ConfigureLectureInstructorProperties(ModelBuilder builder)
        {
            builder.Entity<LectureInstructor>()
                .Property(i => i.LectureId).IsRequired();
            builder.Entity<LectureInstructor>()
                .Property(i => i.InstructorId).IsRequired();
            builder.Entity<LectureInstructor>()
                .Property(i => i.AcademicYearId).IsRequired();
            builder.Entity<LectureInstructor>()
                .Property(i => i.Semester).IsRequired();
            builder.Entity<LectureInstructor>()
                .Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Entity<LectureInstructor>()
                .Property(i => i.CreatedAt).ValueGeneratedOnAdd();
            builder.Entity<LectureInstructor>()
                .Property(i => i.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }

        private static void ConfigureLectureStudentProperties(ModelBuilder builder)
        {
            builder.Entity<LectureStudent>()
                .Property(s => s.LectureId).IsRequired();
            builder.Entity<LectureStudent>()
                .Property(s => s.StudentId).IsRequired();
            builder.Entity<LectureStudent>()
                .Property(s => s.AcademicYearId).IsRequired();
            builder.Entity<LectureStudent>()
                .Property(s => s.Semester).IsRequired();
            builder.Entity<LectureStudent>()
                .Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Entity<LectureStudent>()
                .Property(s => s.CreatedAt).ValueGeneratedOnAdd();
            builder.Entity<LectureStudent>()
                .Property(s => s.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }
    }
}

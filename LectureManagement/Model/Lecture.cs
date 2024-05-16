using Infrastructure.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LectureManagement.Model
{
    public class Lecture: IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public LectureLevel Level { get; set; }
        public Semester Semester { get; set; }
        public LectureType Type { get; set; }
        public float Credit { get; set; }
        public int HoursInWeek { get; set; }
        public int Quota { get; set; }
        public bool IsGroup { get; set; }
        public List<LectureSchedule> Schedules { get; set; }
        public List<LectureInstructor> Instructors { get; set; }
        public List<LectureStudent> Students { get; set; }
        public List<Lecture> Prerequisites { get; set; } = new List<Lecture>();
        public Guid DepartmentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }

    public enum LectureLevel
    {
        Graduate,
        Postgraduate,
        Doctorate
    }
    public enum Semester
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
        Sixth = 6,
        Seventh = 7,
        Eighth = 8
    }

    public enum LectureType
    {
        Compulsory, // Zorunlu
        Elective, // Seçmeli
        VocationalElective // Mesleki Seçmeli
    }

    public enum DayOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
}

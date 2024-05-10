using Infrastructure.Entities.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;
namespace OrganisationManagement.Model
{
    public class Lecture: IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
 #nullable enable
        public string? Description { get; set; }
#nullable disable
        public string Name { get; set; }
        public string Code { get; set; }
        public LectureLevel Level { get; set; }
        public Semester Semester { get; set; }
        public float Credit { get; set; }
        public int HoursInWeek { get; set; }
        public List<Lecture> Prerequisites { get; set; } = new List<Lecture>();
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }

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
}

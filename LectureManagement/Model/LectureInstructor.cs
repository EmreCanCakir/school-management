using Infrastructure.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LectureManagement.Model
{
    public class LectureInstructor: IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid InstructorId { get; set; }
        public Guid LectureId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Lecture Lecture { get; set; }
        public Semester Semester { get; set; }
        public AcademicYear AcademicYear { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}

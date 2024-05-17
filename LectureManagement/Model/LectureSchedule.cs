using Infrastructure.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LectureManagement.Model
{
    public class LectureSchedule: IEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid LectureId { get; set; }
        public Guid ClassroomId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Semester? Semester { get; set; }
        public Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> Schedule { get; set; } = new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>();
        public Lecture Lecture { get; set; }
        public AcademicYear AcademicYear { get; set; }
        
        public void AddSchedule(DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
        {
            Schedule.Add(day, new Tuple<TimeSpan, TimeSpan>(startTime, endTime));
        }
    }
}

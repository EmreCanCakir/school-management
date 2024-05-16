using Infrastructure.Entities.Abstracts;

namespace LectureManagement.Model.Dtos
{
    public class LectureScheduleUpdateDto: IDto
    {
        public Guid Id { get; set; }
        public Guid LectureId { get; set; }
        public Guid ClassroomId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Semester Semester { get; set; }
        public Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>> Schedule { get; set; }
    }
}

using Infrastructure.Entities.Abstracts;

namespace LectureManagement.Model.Dtos
{
    public class LectureInstructorAddDto: IDto
    {
        public Guid LectureId { get; set; }
        public Guid InstructorId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Semester Semester { get; set; }
    }
}

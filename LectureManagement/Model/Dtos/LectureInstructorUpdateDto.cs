using Infrastructure.Entities.Abstracts;

namespace LectureManagement.Model.Dtos
{
    public class LectureInstructorUpdateDto: IDto
    {
        public Guid Id { get; set; }
        public Guid LectureId { get; set; }
        public Guid InstructorId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Semester Semester { get; set; }
    }
}

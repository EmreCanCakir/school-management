using Infrastructure.Entities.Abstracts;

namespace LectureManagement.Model.Dtos
{
    public class LectureStudentUpdateDto: IDto
    {
        public Guid StudentId { get; set; }
        public Guid LectureId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Semester Semester { get; set; }
    }
}

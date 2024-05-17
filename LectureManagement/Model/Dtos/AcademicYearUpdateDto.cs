using Infrastructure.Entities.Abstracts;

namespace LectureManagement.Model.Dtos
{
    public class AcademicYearUpdateDto: IDto
    {
        public Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public AcademicYearStatus Status { get; set; }
    }
}

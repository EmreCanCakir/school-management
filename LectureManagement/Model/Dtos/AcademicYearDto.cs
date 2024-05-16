using Infrastructure.Entities.Abstracts;

namespace LectureManagement.Model.Dtos
{
    public class AcademicYearDto: IDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public AcademicYearStatus Status { get; set; }
    }
}

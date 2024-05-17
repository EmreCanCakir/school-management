using Infrastructure.Entities.Abstracts;

namespace LectureManagement.Model.Dtos
{
    public class LectureUpdateDto: IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public LectureLevel Level { get; set; }
        public Semester Semester { get; set; }
        public LectureType Type { get; set; }
        public float Credit { get; set; }
        public int HoursInWeek { get; set; }
        public int Quota { get; set; }
        public bool IsGroup { get; set; }
        public List<PrerequisiteDto> Prerequisites { get; set; } = new List<PrerequisiteDto>();
        public Guid DepartmentId { get; set; }
    }
}

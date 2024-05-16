using Infrastructure.Entities.Abstracts;

namespace LectureManagement.Model.Dtos
{
    public class LectureAddDto: IDto
    {
        public string? Description { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public LectureLevel Level { get; set; }
        public Semester Semester { get; set; }
        public LectureType Type { get; set; }
        public float Credit { get; set; }
        public int HoursInWeek { get; set; }
        public int Quota { get; set; }
        public bool IsGroup { get; set; }
        public List<Lecture> Prerequisites { get; set; } = new List<Lecture>();
        public Guid DepartmentId { get; set; }
    }
}

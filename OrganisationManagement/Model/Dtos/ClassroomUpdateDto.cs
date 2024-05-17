using Infrastructure.Entities.Abstracts;

namespace OrganisationManagement.Model.Dtos
{
    public class ClassroomUpdateDto: IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public Guid DepartmentId { get; set; }
    }
}

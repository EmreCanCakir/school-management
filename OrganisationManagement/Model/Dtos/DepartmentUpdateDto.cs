using Infrastructure.Entities.Abstracts;

namespace OrganisationManagement.Model.Dtos
{
    public class DepartmentUpdateDto: IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid FacultyId { get; set; }
    }
}

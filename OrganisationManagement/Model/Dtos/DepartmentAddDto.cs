using Infrastructure.Entities.Abstracts;

namespace OrganisationManagement.Model.Dtos
{
    public class DepartmentAddDto: IDto
    {
#nullable disable
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid FacultyId { get; set; }
    }
}

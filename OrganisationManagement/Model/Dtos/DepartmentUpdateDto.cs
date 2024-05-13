using Infrastructure.Entities.Abstracts;

namespace OrganisationManagement.Model.Dtos
{
    public class DepartmentUpdateDto: IDto
    {
        #nullable disable
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid FacultyId { get; set; }
    }
}

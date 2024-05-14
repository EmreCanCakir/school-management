using Infrastructure.Entities.Abstracts;

namespace OrganisationManagement.Model.Dtos
{
    public class FacultyAddDto: IDto
    {
#nullable disable
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

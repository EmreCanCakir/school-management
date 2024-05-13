using Infrastructure.Entities.Abstracts;

namespace OrganisationManagement.Model.Dtos
{
    public class FacultyUpdateDto: IDto
    {
#nullable disable
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

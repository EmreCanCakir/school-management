using Infrastructure.Entities.Abstracts;

namespace OrganisationManagement.Model.Dtos
{
    public class FacultyUpdateDto: IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

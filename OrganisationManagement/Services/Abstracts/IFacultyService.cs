using Infrastructure.Business;
using OrganisationManagement.Model;
using OrganisationManagement.Model.Dtos;

namespace OrganisationManagement.Services.Abstracts
{
    public interface IFacultyService: IBaseService<Faculty, Guid>, IAddService<FacultyAddDto>, IUpdateService<FacultyUpdateDto>
    {
    }
}

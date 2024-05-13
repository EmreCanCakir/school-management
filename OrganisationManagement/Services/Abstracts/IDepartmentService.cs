using Infrastructure.Business;
using OrganisationManagement.Model;
using OrganisationManagement.Model.Dtos;

namespace OrganisationManagement.Services.Abstracts
{
    public interface IDepartmentService: IBaseService<Department, Guid>, IAddService<DepartmentAddDto>, IUpdateService<DepartmentUpdateDto>
    {
    }
}

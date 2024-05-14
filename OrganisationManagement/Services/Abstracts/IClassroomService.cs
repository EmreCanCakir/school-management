using Infrastructure.Business;
using OrganisationManagement.Model;
using OrganisationManagement.Model.Dtos;

namespace OrganisationManagement.Services.Abstracts
{
    public interface IClassroomService: IBaseService<Classroom, Guid>, IAddService<ClassroomAddDto>, IUpdateService<ClassroomUpdateDto>
    {
    }
}

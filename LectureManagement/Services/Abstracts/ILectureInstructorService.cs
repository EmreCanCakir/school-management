using Infrastructure.Business;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;

namespace LectureManagement.Services.Abstracts
{
    public interface ILectureInstructorService: IBaseService<LectureInstructor, Guid>, IAddService<LectureInstructorAddDto>, IUpdateService<LectureInstructorUpdateDto>
    {
    }
}

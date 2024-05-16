using Infrastructure.Business;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;

namespace LectureManagement.Services.Abstracts
{
    public interface ILectureStudentService: IBaseService<LectureStudent, Guid>, IAddService<LectureStudentAddDto>, IUpdateService<LectureStudentUpdateDto>
    {
    }
}

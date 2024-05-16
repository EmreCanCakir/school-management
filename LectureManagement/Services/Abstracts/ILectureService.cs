using Infrastructure.Business;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;

namespace LectureManagement.Services.Abstracts
{
    public interface ILectureService: IBaseService<Lecture, Guid>, IAddService<LectureAddDto>, IUpdateService<LectureUpdateDto>
    {
    }
}

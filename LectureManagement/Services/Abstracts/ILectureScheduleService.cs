using Infrastructure.Business;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;

namespace LectureManagement.Services.Abstracts
{
    public interface ILectureScheduleService: IBaseService<LectureSchedule, Guid>, IAddService<LectureScheduleAddDto>, IUpdateService<LectureScheduleUpdateDto>
    {
    }
}

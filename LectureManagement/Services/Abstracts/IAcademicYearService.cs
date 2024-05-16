using Infrastructure.Business;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;
using IResult = Infrastructure.Utilities.Results.IResult;
namespace LectureManagement.Services.Abstracts
{
    public interface IAcademicYearService: IBaseService<AcademicYear, Guid>, IAddService<AcademicYearDto>, IUpdateService<AcademicYear> 
    {
        Task<IResult> SetStatus(Guid id, AcademicYearStatus status);
    }
}

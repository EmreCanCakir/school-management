using AutoMapper;
using Infrastructure.Utilities.Business;
using Infrastructure.Utilities.Results;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;
namespace LectureManagement.Services.Concretes
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IAcademicYearDal _academicYearDal;
        private readonly IMapper _mapper;

        public AcademicYearService(IAcademicYearDal academicYearDal, IMapper mapper)
        {
            _academicYearDal = academicYearDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(AcademicYearDto entity)
        {
            var academicYear = _mapper.Map<AcademicYear>(entity);
            if(academicYear == null)
            {
                return new ErrorResult("An error occurred while mapping the academic year");
            }

            var validAcademicYear = BusinessRules.Run(
                IsAcademicYearAlreadyExist(academicYear), IsAcademicYearValid(academicYear));

            if (!validAcademicYear.Success)
            {
                return validAcademicYear;
            }

            await _academicYearDal.Add(academicYear);
            return new SuccessResult("Academic Year Created Successfully");
        }

        public async Task<IResult> Delete(Guid id)
        {
            AcademicYear academicYear = GetById(id).Data;

            await _academicYearDal.Delete(academicYear);
            return new SuccessResult("Academic Year Deleted Successfully");
        }

        public IDataResult<List<AcademicYear>> GetAll()
        {
            var academicYears = _academicYearDal.GetAll();
            return new SuccessDataResult<List<AcademicYear>>(academicYears);
        }

        public IDataResult<AcademicYear> GetById(Guid id)
        {
            var academicYear = _academicYearDal.Get(x => x.Id == id);
            if (academicYear == null)
            {
                return new ErrorDataResult<AcademicYear>("Academic Year Not Found");
            }
            return new SuccessDataResult<AcademicYear>(academicYear, "Academic Year Get Successfully");
        }

        public async Task<IResult> SetStatus(Guid id, AcademicYearStatus status)
        {
            var academicYear = GetById(id).Data;
            if (academicYear == null)
            {
                return new ErrorResult("Academic Year Not Found");
            }

            academicYear.Status = status;
            await _academicYearDal.Update(academicYear, academicYear.Id);
            return new SuccessResult($"Academic Year Status: {status.ToString()}, Updated Successfully");
        }

        public async Task<IResult> Update(AcademicYearUpdateDto entity)
        {
            var academicYear = _mapper.Map<AcademicYear>(entity);
            var validAcademicYear = IsAcademicYearValid(academicYear);

            if (!validAcademicYear.Success)
            {
                return validAcademicYear;
            }

            await _academicYearDal.Update(academicYear, academicYear.Id);
            return new SuccessResult("Academic Year Updated Successfully");
        }

        private IResult IsAcademicYearAlreadyExist(AcademicYear academicYear)
        {
            var academicYears = _academicYearDal.GetAll(
                x => x.StartDate.Year == academicYear.StartDate.Year || x.EndDate.Year == academicYear.EndDate.Year);

            if (academicYears.Any())
            {
                return new ErrorResult("Academic Year Already Exists");
            }
            return new SuccessResult();
        }

        private IResult IsAcademicYearValid(AcademicYear academicYear)
        {
            if(academicYear.StartDate >= academicYear.EndDate)
            {
                return new ErrorResult("Academic Year Start Date Must Be Less Than End Date");
            }

            if(academicYear.StartDate.Year != academicYear.EndDate.Year -1)
            {
                return new ErrorResult("Academic Year End Date must be 1 year older than the start date");
            }

            return new SuccessResult();
        }
    }
}

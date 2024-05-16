using Infrastructure.Utilities.Results;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;
using Infrastructure.Utilities.Business;
using AutoMapper;
namespace LectureManagement.Services.Concretes
{
    public class LectureScheduleService : ILectureScheduleService
    {
        private readonly ILectureScheduleDal _lectureScheduleDal;
        private readonly IAcademicYearDal _academicYearDal;
        private readonly IMapper _mapper;
        public LectureScheduleService(ILectureScheduleDal lectureScheduleDal, IMapper mapper, IAcademicYearDal academicYearDal)
        {
            _lectureScheduleDal = lectureScheduleDal;
            _mapper = mapper;
            _academicYearDal = academicYearDal;
        }

        public async Task<IResult> Add(LectureScheduleAddDto entity)
        {
            var lectureSchedule = _mapper.Map<LectureSchedule>(entity);
            if (lectureSchedule == null)
            {
                return new ErrorResult("An error occurred while mapping the lecture schedule");
            }

            var lectureScheduleValid = BusinessRules.Run(
                    IsScheduleTimeValidWithClassroom(lectureSchedule),
                    IsScheduledTimeSuitableWithWeeklyHours(lectureSchedule),
                    IsSemesterSameWithLectureSemester(lectureSchedule),
                    IsAcademicYearLatest(lectureSchedule));

            if (!lectureScheduleValid.Success)
            {
                return lectureScheduleValid;
            }

            await _lectureScheduleDal.Add(lectureSchedule);
            return new SuccessResult("Lecture Schedule Created Successfully");
        }

        public async Task<IResult> Delete(Guid id)
        {
            var lectureSchedule = GetById(id).Data;
            if (lectureSchedule == null)
            {
                return new ErrorResult("Lecture Schedule Not Found");
            }

            await _lectureScheduleDal.Delete(lectureSchedule);
            return new SuccessResult("Lecture Schedule Deleted Successfully");
        }

        public IDataResult<List<LectureSchedule>> GetAll()
        {
            var lectureSchedules = _lectureScheduleDal.GetAll();
            return new SuccessDataResult<List<LectureSchedule>>(lectureSchedules, "Lecture Schedules Retrieved Successfully");
        }

        public IDataResult<LectureSchedule> GetById(Guid id)
        {
            var lectureSchedule = _lectureScheduleDal.Get(x => x.Id == id);
            if (lectureSchedule == null)
            {
                return new ErrorDataResult<LectureSchedule>("Lecture Schedule Not Found");
            }

            return new SuccessDataResult<LectureSchedule>(lectureSchedule, "Lecture Schedule Retrieved Successfully");
        }

        public async Task<IResult> Update(LectureScheduleUpdateDto entity)
        {
            var lectureSchedule = _mapper.Map<LectureSchedule>(entity);
            if (lectureSchedule == null)
            {
                return new ErrorResult("An error occurred while mapping the lecture schedule");
            }

            var lectureScheduleValid = ValidateUpdatedProperties(lectureSchedule);

            if (!lectureScheduleValid.Success)
            {
                return lectureScheduleValid;
            }

            await _lectureScheduleDal.Update(lectureSchedule);
            return new SuccessResult("Lecture Schedule Updated Successfully");
        }

        private IResult IsScheduleTimeValidWithClassroom(LectureSchedule lectureSchedule)
        {
            var lectureSchedulesByAcademicYearAndClassroom = GetAll().Data?.FindAll(
                x => x.AcademicYear == lectureSchedule.AcademicYear &&
                x.Semester == lectureSchedule.Semester &&
                x.ClassroomId == lectureSchedule.ClassroomId &&
                x.LectureId != lectureSchedule.LectureId);

            if (lectureSchedulesByAcademicYearAndClassroom == null || !lectureSchedulesByAcademicYearAndClassroom.Any())
            {
                return new SuccessResult();
            }

            var schedules = lectureSchedulesByAcademicYearAndClassroom.SelectMany(x => x.Schedule).ToList();

            return Helpers.Helper.CompareSchedules(lectureSchedule.Schedule, schedules,
                "Classroom will serve another lecture during the relevant time interval. Please change the time interval");
        }

        private IResult IsSemesterSameWithLectureSemester(LectureSchedule lectureSchedule)
        {
            var lecture = GetById(lectureSchedule.Id).Data.Lecture;
            return Helpers.Helper.IsSemesterSameWithLectureSemester(lecture, lectureSchedule.Semester);
        }

        private IResult IsAcademicYearLatest(LectureSchedule lectureSchedule)
        {
            var academicYears = _academicYearDal.GetAll();

            var selectedAcademicYear = academicYears.FirstOrDefault(x => x.Id == lectureSchedule.AcademicYearId);
            if (selectedAcademicYear == null)
            {
                return new ErrorResult("Academic Year Not Found");
            }

            return Helpers.Helper.IsAcademicYearLatest(academicYears, lectureSchedule.AcademicYearId);
        }

        private IResult IsScheduledTimeSuitableWithWeeklyHours(LectureSchedule lectureSchedule)
        {
            if (!lectureSchedule.Schedule.Any())
            {
                return new ErrorResult("There is no scheduled time");
            }

            var totalHours = lectureSchedule.Schedule.Sum(x => (x.Value.Item2 - x.Value.Item1).TotalHours);
            if (totalHours > lectureSchedule.Lecture.HoursInWeek)
            {
                return new ErrorResult("The total hours of the scheduled time cannot be greater than the weekly hours of the course");
            }

            if (totalHours < lectureSchedule.Lecture.HoursInWeek)
            {
                return new ErrorResult("The total hours of the scheduled time cannot be less than the weekly hours of the course");
            }

            return new SuccessResult();
        }

        private IResult ValidateUpdatedProperties(LectureSchedule lectureSchedule)
        {
            if (IsScheduleChanged(lectureSchedule))
            {
                var isValidSchedule = BusinessRules.Run(
                    IsScheduleTimeValidWithClassroom(lectureSchedule),
                    IsScheduledTimeSuitableWithWeeklyHours(lectureSchedule));

                if (!isValidSchedule.Success)
                {
                    return isValidSchedule;
                }
            }

            if (lectureSchedule.Semester != null)
            {
                var isValidSemester = IsSemesterSameWithLectureSemester(lectureSchedule);
                if (!isValidSemester.Success)
                {
                    return isValidSemester;
                }
            }

            if (lectureSchedule.AcademicYear != null)
            {
                var isValidAcademicYear = IsAcademicYearLatest(lectureSchedule);
                if (!isValidAcademicYear.Success)
                {
                    return isValidAcademicYear;
                }
            }

            return new SuccessResult();
        }


        private bool IsScheduleChanged(LectureSchedule lectureSchedule)
        {
            if (!lectureSchedule.Schedule.Any())
            {
                return false;
            }

            return lectureSchedule.Schedule != GetById(lectureSchedule.Id).Data?.Schedule;
        }
    }
}

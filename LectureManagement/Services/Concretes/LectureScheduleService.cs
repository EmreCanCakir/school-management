using Infrastructure.Utilities.Results;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;
using Infrastructure.Utilities.Business;
using AutoMapper;
using MassTransit;
using Contracts;
namespace LectureManagement.Services.Concretes
{
    public class LectureScheduleService : ILectureScheduleService
    {
        private readonly ILectureScheduleDal _lectureScheduleDal;
        private readonly IAcademicYearDal _academicYearDal;
        private readonly ILectureDal _lectureDal;
        private readonly IMapper _mapper;
        private readonly ClassroomDetailResponseService _classroomDetailResponseService;
        public LectureScheduleService(ILectureScheduleDal lectureScheduleDal, IMapper mapper, IAcademicYearDal academicYearDal, ILectureDal lectureDal, ClassroomDetailResponseService classroomDetailResponseService)
        {
            _lectureScheduleDal = lectureScheduleDal;
            _mapper = mapper;
            _academicYearDal = academicYearDal;
            _lectureDal = lectureDal;
            _classroomDetailResponseService = classroomDetailResponseService;
        }

        public async Task<IResult> Add(LectureScheduleAddDto entity)
        {
            var lectureSchedule = _mapper.Map<LectureSchedule>(entity);
            if (lectureSchedule == null)
            {
                return new ErrorResult("An error occurred while mapping the lecture schedule");
            }

            var lectureScheduleValid = BusinessRules.Run(
                    IsLectureAlreadyScheduledInSameAcademicYear(lectureSchedule),
                    IsScheduleTimeValidWithClassroom(lectureSchedule),
                    IsScheduledTimeSuitableWithWeeklyHours(lectureSchedule),
                    IsSemesterSameWithLectureSemester(lectureSchedule),
                    IsAcademicYearLatest(lectureSchedule),
                    await IsLectureQuotaSuitableWithClassroomCapacity(lectureSchedule),
                    await IsLectureAndClassroomInSameDepartment(lectureSchedule));

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

            await _lectureScheduleDal.Update(lectureSchedule, lectureSchedule.Id);
            return new SuccessResult("Lecture Schedule Updated Successfully");
        }

        private IResult IsScheduleTimeValidWithClassroom(LectureSchedule lectureSchedule)
        {
            var lectureSchedulesByAcademicYearAndClassroom = GetAll().Data?.FindAll(
                x => x.AcademicYearId == lectureSchedule.AcademicYearId &&
                x.Semester == lectureSchedule.Semester &&
                x.ClassroomId == lectureSchedule.ClassroomId);

            if (lectureSchedule.Id != Guid.Empty)
            {
                lectureSchedulesByAcademicYearAndClassroom = lectureSchedulesByAcademicYearAndClassroom?
                    .Where(x => x.LectureId != lectureSchedule.LectureId).ToList();
            }

            if (lectureSchedulesByAcademicYearAndClassroom == null || !lectureSchedulesByAcademicYearAndClassroom.Any())
            {
                return new SuccessResult();
            }

            var schedules = lectureSchedulesByAcademicYearAndClassroom.SelectMany(x => x.Schedule).ToList();

            return Helpers.Helper.CompareSchedules(lectureSchedule.Schedule, schedules,
                "Classroom will serve another lecture during the relevant time interval. Please change the time interval");
        }

        private async Task<IResult> IsLectureQuotaSuitableWithClassroomCapacity(LectureSchedule lectureSchedule)
        {
            var lecture = _lectureDal.Get(x => x.Id == lectureSchedule.LectureId);
            if (lecture == null)
            {
                return new ErrorResult("Lecture Not Found");
            }

            var classroomDetail = await _classroomDetailResponseService.GetClassroomDetailAsync(lectureSchedule.ClassroomId);
            if (!classroomDetail.Success)
            {
                return new ErrorResult(classroomDetail.Message);
            }

            if (classroomDetail.Data.Capacity < lecture.Quota)
            {
                return new ErrorResult("The quota of the lecture cannot be greater than the capacity of the classroom");
            }

            return new SuccessResult();
        }

        private async Task<IResult> IsLectureAndClassroomInSameDepartment(LectureSchedule lectureSchedule)
        {
            var lecture = _lectureDal.Get(x => x.Id == lectureSchedule.LectureId);
            if (lecture == null)
            {
                return new ErrorResult("Lecture Not Found");
            }

            var classroomDetail = await _classroomDetailResponseService.GetClassroomDetailAsync(lectureSchedule.ClassroomId);
            if (!classroomDetail.Success)
            {
                return new ErrorResult(classroomDetail.Message);
            }

            if (classroomDetail.Data.DepartmentId != lecture.DepartmentId)
            {
                return new ErrorResult("Lecture cannot be scheduled for a classroom in a different department");
            }

            return new SuccessResult();
        }

        private IResult IsLectureAlreadyScheduledInSameAcademicYear(LectureSchedule lectureSchedule)
        {
            var lectureSchedules = GetAll().Data?.FindAll(
                x => x.AcademicYearId == lectureSchedule.AcademicYearId &&
                x.Semester == lectureSchedule.Semester &&
                x.LectureId == lectureSchedule.LectureId);

            if (lectureSchedules != null && lectureSchedules.Any())
            {
                return new ErrorResult("The lecture is already scheduled in the same academic year and semester");
            }

            return new SuccessResult();
        }

        private IResult IsSemesterSameWithLectureSemester(LectureSchedule lectureSchedule)
        {
            var lecture = _lectureDal.Get(x => x.Id == lectureSchedule.LectureId);
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
            var lecture = _lectureDal.Get(x => x.Id == lectureSchedule.LectureId);
            if (totalHours > lecture.HoursInWeek)
            {
                return new ErrorResult("The total hours of the scheduled time cannot be greater than the weekly hours of the course");
            }

            if (totalHours < lecture.HoursInWeek)
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

using AutoMapper;
using Infrastructure.Utilities.Business;
using Infrastructure.Utilities.Results;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.DataAccess.Concretes;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;
namespace LectureManagement.Services.Concretes
{
    public class LectureInstructorService : ILectureInstructorService
    {
        private readonly ILectureInstructorDal _lectureInstructorDal;
        private readonly ILectureDal _lectureDal;
        private readonly IMapper _mapper;
        private readonly IAcademicYearDal _academicYearDal;

        public LectureInstructorService(ILectureInstructorDal lectureInstructorDal, IMapper mapper, ILectureDal lectureDal, IAcademicYearDal academicYearDal)
        {
            _lectureInstructorDal = lectureInstructorDal;
            _mapper = mapper;
            _lectureDal = lectureDal;
            _academicYearDal = academicYearDal;
        }

        public async Task<IResult> Add(LectureInstructorAddDto entity)
        {
            var lectureInstructor = _mapper.Map<LectureInstructor>(entity);
            if (lectureInstructor == null)
            {
                return new ErrorResult("An error occurred while mapping the lecture instructor");
            }

            var businessRules = BusinessRules.Run(
                               IsInstructorAvailable(lectureInstructor),
                               IsAcademicYearLatest(lectureInstructor),
                               IsSemesterSameWithLectureSemester(lectureInstructor),
                               IsLectureAlreadyAssignedToInstructor(lectureInstructor),
                               IsLectureScheduleHasSchedule(lectureInstructor));

            if (!businessRules.Success)
            {
                return businessRules;
            }

            await _lectureInstructorDal.Add(lectureInstructor);
            return new SuccessResult("Lecture Instructor Created Successfully");
        }

        public async Task<IResult> Delete(Guid id)
        {
            var lectureInstructor = GetById(id).Data;
            if (lectureInstructor == null)
            {
                return new ErrorResult("Lecture Instructor Not Found");
            }

            await _lectureInstructorDal.Delete(lectureInstructor);
            return new SuccessResult("Lecture Instructor Deleted Successfully");
        }

        public IDataResult<List<LectureInstructor>> GetAll()
        {
            var lectureInstructors = _lectureInstructorDal.GetAll();
            return new SuccessDataResult<List<LectureInstructor>>(lectureInstructors, "Lecture Instructors Listed Successfully");
        }

        public IDataResult<LectureInstructor> GetById(Guid id)
        {
            var lectureInstructor = _lectureInstructorDal.Get(x => x.Id == id);
            return new SuccessDataResult<LectureInstructor>(lectureInstructor, "Lecture Instructor Retrieved Successfully");
        }

        public async Task<IResult> Update(LectureInstructorUpdateDto entity)
        {
            var lectureInstructor = _mapper.Map<LectureInstructor>(entity);
            if (lectureInstructor == null)
            {
                return new ErrorResult("An error occurred while mapping the lecture instructor");
            }

            var businessRules = BusinessRules.Run(
                               IsInstructorAvailable(lectureInstructor),
                               IsAcademicYearLatest(lectureInstructor),
                               IsSemesterSameWithLectureSemester(lectureInstructor),
                               IsLectureAlreadyAssignedToInstructor(lectureInstructor),
                               IsLectureScheduleHasSchedule(lectureInstructor));

            if (!businessRules.Success)
            {
                return businessRules;
            }

            await _lectureInstructorDal.Update(lectureInstructor);
            return new SuccessResult("Lecture Instructor Updated Successfully");
        }

        private IResult IsInstructorAvailable(LectureInstructor lectureInstructor)
        {
            var lecture = _lectureDal.Get(x => x.Id == lectureInstructor.LectureId);
            if (lecture == null)
            {
                return new ErrorResult("Lecture Not Found");
            }

            float totalCredit = lectureInstructor.Id == Guid.Empty ? lecture.Credit : 0;
            int totalHours = lectureInstructor.Id == Guid.Empty ? lecture.HoursInWeek : 0;

            var lectureInstructors = _lectureInstructorDal.GetAll(
                x => x.InstructorId == lectureInstructor.InstructorId &&
                x.AcademicYearId == lectureInstructor.AcademicYearId &&
                x.Semester == lectureInstructor.Semester);

            foreach (var item in lectureInstructors)
            {
                totalCredit += item.Lecture.Credit;
                totalHours += item.Lecture.HoursInWeek;
            }


            if (totalCredit > 30)
            {
                return new ErrorResult($"Instructor Credit Exceeded, Total Credit cannot greater than 30, Instructor Credit is {totalCredit}");
            }
            if (totalHours > 30)
            {
                return new ErrorResult($"Instructor Hours In Week Exceeded, Total Lecture Hours cannot greater than 30, Instructor Hours is {totalHours}");
            }

            return new SuccessResult();
        }

        private IResult IsAcademicYearLatest(LectureInstructor lectureInstructor)
        {
            var academicYears = _academicYearDal.GetAll();
            var selectedAcademicYear = academicYears.FirstOrDefault(x => x.Id == lectureInstructor.AcademicYearId);
            if (selectedAcademicYear == null)
            {
                return new ErrorResult("Academic Year Not Found");
            }

            return Helpers.Helper.IsAcademicYearLatest(academicYears, lectureInstructor.AcademicYearId);
        }

        private IResult IsSemesterSameWithLectureSemester(LectureInstructor lectureInstructor)
        {
            var lecture = _lectureDal.Get(x => x.Id == lectureInstructor.LectureId);
            if (lecture == null)
            {
                return new ErrorResult("Lecture Not Found, please select a lecture");
            }

            return Helpers.Helper.IsSemesterSameWithLectureSemester(lecture, lectureInstructor.Semester);
        }

        private IResult IsLectureAlreadyAssignedToInstructor(LectureInstructor lectureInstructor)
        {
            var existingLectureInstructor = _lectureInstructorDal.Get(
                x => x.LectureId == lectureInstructor.LectureId &&
                x.AcademicYearId == lectureInstructor.AcademicYearId &&
                x.Semester == lectureInstructor.Semester);

            if (lectureInstructor.Id == Guid.Empty && existingLectureInstructor != null)
            {
                return new ErrorResult("That lecture has already been assigned a teacher");
            }

            return new SuccessResult();
        }

        private IResult IsLectureScheduleHasSchedule(LectureInstructor lectureInstructor)
        {
            var lectureSchedules = _lectureDal.Get(x => x.Id == lectureInstructor.LectureId).Schedules;
            var activeYearSchedules = lectureSchedules?.FindAll(x => x.AcademicYearId == lectureInstructor.AcademicYearId);

            if (activeYearSchedules == null || !activeYearSchedules.Any())
            {
                return new ErrorResult("There is no scheduled time for the course. Please schedule a time before assign a lecturer");
            }

            return new SuccessResult();
        }
    }
}

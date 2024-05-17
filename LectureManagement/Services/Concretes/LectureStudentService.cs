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
    public class LectureStudentService : ILectureStudentService
    {
        private readonly ILectureStudentDal _lectureStudentDal;
        private readonly IAcademicYearDal _academicYearDal;
        private readonly IMapper _mapper;
        private readonly ILectureDal _lectureDal;

        public LectureStudentService(ILectureStudentDal lectureStudentDal, IMapper mapper, ILectureDal lectureDal, IAcademicYearDal academicYearDal)
        {
            _lectureStudentDal = lectureStudentDal;
            _mapper = mapper;
            _lectureDal = lectureDal;
            _academicYearDal = academicYearDal;
        }

        public async Task<IResult> Add(LectureStudentAddDto entity)
        {
            var lectureStudent = _mapper.Map<LectureStudent>(entity);
            if (lectureStudent == null)
            {
                return new ErrorResult("An error occurred while mapping the lecture student");
            }

            var businessRules = BusinessRules.Run(
                               IsStudentAlreadyRegisteredToLecture(lectureStudent),
                               IsQuotaExceeded(lectureStudent),
                               IsAcademicYearLatest(lectureStudent),
                               IsSemesterSameWithLectureSemester(lectureStudent),
                               IsCreditExceeded(lectureStudent),
                               HasConflictedLecture(lectureStudent),
                               IsPassedPrerequisities(lectureStudent));

            if (!businessRules.Success)
            {
                return businessRules;
            }

            await _lectureStudentDal.Add(lectureStudent);
            return new SuccessResult("Lecture Student Created Successfully");
        }

        public async Task<IResult> Delete(Guid id)
        {
            var LectureStudent = GetById(id).Data;
            if (LectureStudent == null)
            {
                return new ErrorResult("Lecture Student Not Found");
            }

            await _lectureStudentDal.Delete(LectureStudent);
            return new SuccessResult("Lecture Student Deleted Successfully");
        }

        public IDataResult<List<LectureStudent>> GetAll()
        {
            var result = _lectureStudentDal.GetAll();
            return new SuccessDataResult<List<LectureStudent>>(result, "Lecture Students Listed Successfully");
        }

        public IDataResult<LectureStudent> GetById(Guid id)
        {
            var result = _lectureStudentDal.Get(x => x.Id == id);
            return new SuccessDataResult<LectureStudent>(result, "Lecture Student Retrieved Successfully");
        }

        public async Task<IResult> Update(LectureStudentUpdateDto entity)
        {
            var lectureStudent = _mapper.Map<LectureStudent>(entity);
            if (lectureStudent == null)
            {
                return new ErrorResult("Lecture Student Not Found");
            }

            var businessRules = BusinessRules.Run(
                               IsStudentAlreadyRegisteredToLecture(lectureStudent),
                               IsQuotaExceeded(lectureStudent),
                               IsAcademicYearLatest(lectureStudent),
                               IsSemesterSameWithLectureSemester(lectureStudent),
                               IsCreditExceeded(lectureStudent),
                               HasConflictedLecture(lectureStudent),
                               IsPassedPrerequisities(lectureStudent));

            if (!businessRules.Success)
            {
                return businessRules;
            }

            await _lectureStudentDal.Update(lectureStudent, lectureStudent.Id);
            return new SuccessResult("Lecture Student Updated Successfully");
        }

        private IResult IsStudentAlreadyRegisteredToLecture(LectureStudent lectureStudent)
        {
            var existingLectureStudent = _lectureStudentDal.Get(
                x => x.StudentId == lectureStudent.StudentId && x.LectureId == lectureStudent.LectureId &&
                x.Semester == lectureStudent.Semester && x.AcademicYearId == lectureStudent.AcademicYearId);

            if (existingLectureStudent != null &&
                (lectureStudent.Id == Guid.Empty || existingLectureStudent.Id != lectureStudent.Id))
            {
                return new ErrorResult("Student is already registered to this lecture");
            }

            return new SuccessResult();
        }

        private IResult IsQuotaExceeded(LectureStudent lectureStudent)
        {
            var lecture = _lectureDal.Get(x => x.Id == lectureStudent.LectureId);
            if (lecture == null)
            {
                return new ErrorResult("Lecture is not found");
            }

            var lectureStudentCount = _lectureStudentDal.GetAll(x =>
                   x.LectureId == lectureStudent.LectureId && x.Semester == lectureStudent.Semester &&
                   x.AcademicYearId == lectureStudent.AcademicYearId && 
                   x.StudentId != lectureStudent.StudentId).Count;

            int currentPerson = 1;

            if (lectureStudentCount + currentPerson > lecture.Quota)
            {
                return new ErrorResult($"Quota exceeded for this lecture. Lecture is allowed only for {lecture.Quota}");
            }

            return new SuccessResult();
        }

        private IResult IsCreditExceeded(LectureStudent lectureStudent)
        {
            var currentStudentLectures = _lectureStudentDal.GetAll(
                   x => x.StudentId == lectureStudent.StudentId && x.Semester == lectureStudent.Semester &&
                   x.AcademicYearId == lectureStudent.AcademicYearId && x.LectureId != lectureStudent.LectureId);

            if (currentStudentLectures == null || !currentStudentLectures.Any())
            {
                return new SuccessResult();
            }

            var totalCredit = currentStudentLectures.Sum(x => x.Lecture.Credit);
            var lecture = _lectureDal.Get(x => x.Id == lectureStudent.LectureId);

            if (lecture == null)
            {
                return new ErrorResult("Lecture is not found");
            }

            if (totalCredit + lecture.Credit > 45)
            {
                return new ErrorResult("Credit limit exceeded. Student can take maximum 45 credits in a semester");
            }

            return new SuccessResult();
        }

        private IResult HasConflictedLecture(LectureStudent lectureStudent)
        {
            var currentStudentLectures = _lectureStudentDal.GetAll(
                        x => x.StudentId == lectureStudent.StudentId && x.Semester == lectureStudent.Semester &&
                        x.AcademicYearId == lectureStudent.AcademicYearId);

            if (currentStudentLectures == null || !currentStudentLectures.Any())
            {
                return new SuccessResult();
            }

            if (lectureStudent.Id != Guid.Empty)
            {
                currentStudentLectures = currentStudentLectures.Where(x => x.Id != lectureStudent.Id).ToList();
            }

            var savedLectureSchedules = currentStudentLectures.SelectMany(
                x => x.Lecture.Schedules.SelectMany(s=> s.Schedule)).ToList();

            var lecture = _lectureDal.Get(x => x.Id == lectureStudent.LectureId);
            if (lecture == null)
            {
                return new ErrorResult("Lecture is not found");
            }

            var currentSchedules = lecture.Schedules.FirstOrDefault(
                                        s=> s.AcademicYearId == lectureStudent.AcademicYearId && 
                                        s.Semester == lectureStudent.Semester)?.Schedule;

            if (currentSchedules == null)
            {
                return new ErrorResult("The programme of the lecture to be selected was not found");
            }

            return Helpers.Helper.CompareSchedules(
                currentSchedules, savedLectureSchedules, 
                "Lectures overlap, please take another course or drop the conflicting course");
        }

        private IResult IsPassedPrerequisities(LectureStudent lectureStudent)
        {
            var lecture = _lectureDal.Get(x => x.Id == lectureStudent.LectureId);
            if (lecture == null)
            {
                return new ErrorResult("Lecture is not found");
            }

            string prerequisiteLectureCode = "";
            var isPassed = lecture.Prerequisites.All(prerequisite =>
            {
                var passedLecture = _lectureStudentDal.Get(
                    x => x.StudentId == lectureStudent.StudentId && x.LectureId == prerequisite.Id &&
                    ((x.AcademicYearId == lectureStudent.AcademicYearId && x.Semester < lectureStudent.Semester) ||
                    (x.AcademicYearId != lectureStudent.AcademicYearId)));

                if (passedLecture == null)
                {
                    prerequisiteLectureCode = _lectureDal.Get(x => x.Id == prerequisite.Id).Code;
                    return false;
                }

                return true;
            });

            if (!isPassed)
            {
                return new ErrorResult($"Prerequisites are not passed, please before take the {prerequisiteLectureCode}.");
            }

            return new SuccessResult();
        }

        private IResult IsAcademicYearLatest(LectureStudent lectureStudent)
        {
            var academicYears = _academicYearDal.GetAll();
            var selectedAcademicYear = academicYears.FirstOrDefault(x => x.Id == lectureStudent.AcademicYearId);
            if (selectedAcademicYear == null)
            {
                return new ErrorResult("Academic Year Not Found");
            }

            return Helpers.Helper.IsAcademicYearLatest(academicYears, lectureStudent.AcademicYearId);
        }

        private IResult IsSemesterSameWithLectureSemester(LectureStudent lectureStudent)
        {
            var lecture = _lectureDal.Get(x => x.Id == lectureStudent.LectureId);
            if (lecture == null)
            {
                return new ErrorResult("Lecture Not Found, please select a lecture");
            }

            return Helpers.Helper.IsSemesterSameWithLectureSemester(lecture, lectureStudent.Semester);
        }
    }
}

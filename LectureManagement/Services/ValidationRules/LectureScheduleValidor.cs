using FluentValidation;
using LectureManagement.Model;

namespace LectureManagement.Services.ValidationRules
{
    public class LectureScheduleValidor: AbstractValidator<LectureSchedule>
    {
        public LectureScheduleValidor()
        {
            RuleFor(ls => ls.LectureId).NotEmpty().WithMessage("Lecture ID cannot be empty.");
            RuleFor(ls => ls.ClassroomId).NotEmpty().WithMessage("Classroom ID cannot be empty.");
            RuleFor(ls => ls.AcademicYearId).NotEmpty().WithMessage("Academic year ID cannot be empty.");
            RuleFor(ls => ls.Semester).NotEmpty().WithMessage("Semester cannot be empty.");
        }
    }
}

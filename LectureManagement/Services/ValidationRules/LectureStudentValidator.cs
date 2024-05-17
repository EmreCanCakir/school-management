using FluentValidation;
using LectureManagement.Model;

namespace LectureManagement.Services.ValidationRules
{
    public class LectureStudentValidator: AbstractValidator<LectureStudent>
    {
        public LectureStudentValidator()
        {
            RuleFor(ls => ls.LectureId).NotEmpty().WithMessage("Lecture ID cannot be empty.");
            RuleFor(ls => ls.StudentId).NotEmpty().WithMessage("Student ID cannot be empty.");
            RuleFor(ls => ls.AcademicYearId).NotEmpty().WithMessage("Academic year ID cannot be empty.");
            RuleFor(ls => ls.Semester).NotEmpty().WithMessage("Semester cannot be empty.");
        }
    }
}

using FluentValidation;
using LectureManagement.Model;

namespace LectureManagement.Services.ValidationRules
{
    public class LectureInstructorValidator : AbstractValidator<LectureInstructor>
    {
        public LectureInstructorValidator()
        {
            RuleFor(li => li.LectureId).NotEmpty().WithMessage("Lecture ID cannot be empty.");
            RuleFor(li => li.InstructorId).NotEmpty().WithMessage("Instructor ID cannot be empty.");
            RuleFor(li => li.AcademicYearId).NotEmpty().WithMessage("Academic year ID cannot be empty.");
            RuleFor(li => li.Semester).NotEmpty().WithMessage("Semester cannot be empty.");
        }
    }
}

using FluentValidation;
using LectureManagement.Model;

namespace LectureManagement.Services.ValidationRules
{
    public class AcademicYearValidator: AbstractValidator<AcademicYear>
    {
        public AcademicYearValidator()
        {
            RuleFor(ay => ay.StartDate).NotEmpty().WithMessage("Start date cannot be empty.");
            RuleFor(ay => ay.EndDate).NotEmpty().WithMessage("End date cannot be empty.");
            RuleFor(ay => ay.EndDate).GreaterThan(ay => ay.StartDate).WithMessage("End date must be greater than start date.");
            RuleFor(ay => ay.Status).NotEmpty().WithMessage("Status cannot be empty.");
            RuleFor(ay => ay.Status).IsInEnum().WithMessage("Status must be a valid AcademicYearStatus.");
        }
    }
}

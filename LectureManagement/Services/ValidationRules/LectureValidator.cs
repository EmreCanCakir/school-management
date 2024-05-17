using FluentValidation;
using LectureManagement.Model;

namespace LectureManagement.Services.ValidationRules
{
    public class LectureValidator : AbstractValidator<Lecture>
    {
        public LectureValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Lecture name cannot be empty.");
            RuleFor(c => c.Name).MinimumLength(3).WithMessage("Lecture name must be at least 3 characters long.");
            RuleFor(c => c.Name).Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Lecture name can only contain letters, numbers and spaces.");
            RuleFor(c => c.Name).MaximumLength(50).WithMessage("Lecture name cannot be longer than 50 characters.");

            RuleFor(f => f.Code).NotEmpty().WithMessage("Lecture code cannot be empty");
            RuleFor(f => f.Code).MinimumLength(2).WithMessage("Lecture code must be at least 2 characters");
            RuleFor(f => f.Code).Matches(@"^[a-zA-Z0-9]*$").WithMessage("Lecture code can only contain letters and numbers");
            RuleFor(f => f.Code).MaximumLength(10).WithMessage("Lecture code cannot be longer than 10 characters");

            RuleFor(x => x.Credit).NotEmpty().WithMessage("Credit cannot be empty");
            RuleFor(x => x.Credit).GreaterThan(0).WithMessage("Credit must be greater than 0");
            RuleFor(x => x.Credit).LessThanOrEqualTo(10).WithMessage("Credit cannot be greater than 10");

            RuleFor(x => x.HoursInWeek).NotEmpty().WithMessage("Hours in week cannot be empty");
            RuleFor(x => x.HoursInWeek).GreaterThan(0).WithMessage("Hours in week must be greater than 0");
            RuleFor(x => x.HoursInWeek).LessThanOrEqualTo(20).WithMessage("Hours in week cannot be greater than 20");

            RuleFor(x => x.Quota).NotEmpty().WithMessage("Quota cannot be empty");
            RuleFor(x => x.Quota).GreaterThan(0).WithMessage("Quota must be greater than 0");
            RuleFor(x => x.Quota).LessThanOrEqualTo(120).WithMessage("Quota cannot be greater than 120");

            RuleFor(x => x.Level).NotEmpty().WithMessage("Level cannot be empty");
            RuleFor(x => x.Level).IsInEnum().WithMessage("Level must be Graduate, Postgraduate or Doctorate");

            RuleFor(x => x.Semester).NotEmpty().WithMessage("Semester cannot be empty");
            RuleFor(x => x.Semester).IsInEnum().WithMessage("Semester must be First, Second, Third, Fourth, Fifth, Sixth, Seventh or Eighth");

            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("Department cannot be empty");
        }
    }
}

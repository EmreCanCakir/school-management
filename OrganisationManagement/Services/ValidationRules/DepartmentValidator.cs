using FluentValidation;
using OrganisationManagement.Model;

namespace OrganisationManagement.Services.ValidationRules
{
    public class DepartmentValidator: AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Department name cannot be empty");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Department name must be at least 3 characters");
            RuleFor(x => x.Name).Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Department name can only contain letters, numbers and spaces");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Department name cannot be longer than 50 characters");

            RuleFor(x => x.Code).NotEmpty().WithMessage("Department code cannot be empty");
            RuleFor(x => x.Code).MinimumLength(2).WithMessage("Department code must be at least 2 characters");
            RuleFor(x => x.Code).Matches(@"^[a-zA-Z0-9]*$").WithMessage("Department code can only contain letters and numbers");
            RuleFor(x => x.Code).MaximumLength(10).WithMessage("Department code cannot be longer than 10 characters");

            RuleFor(x => x.FacultyId).NotEmpty().WithMessage("Faculty ID cannot be empty");
        }
    }
}

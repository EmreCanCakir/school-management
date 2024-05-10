using FluentValidation;
using OrganisationManagement.Model;

namespace OrganisationManagement.Services.ValidationRules
{
    public class ClassroomValidator: AbstractValidator<Classroom>
    {
        public ClassroomValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Classroom name cannot be empty.");
            RuleFor(c => c.Name).MinimumLength(3).WithMessage("Classroom name must be at least 3 characters long.");
            RuleFor(c => c.Name).Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Classroom name can only contain letters, numbers and spaces.");
            RuleFor(c => c.Name).MaximumLength(50).WithMessage("Classroom name cannot be longer than 50 characters.");

            RuleFor(c => c.Code).NotEmpty().WithMessage("Classroom code cannot be empty.");
            RuleFor(c => c.Code).MinimumLength(2).WithMessage("Classroom code must be at least 2 characters long.");
            RuleFor(c => c.Code).Matches(@"^[a-zA-Z0-9]*$").WithMessage("Classroom code can only contain letters and numbers.");
            RuleFor(c => c.Code).MaximumLength(10).WithMessage("Classroom code cannot be longer than 10 characters.");

            RuleFor(c => c.Capacity).NotEmpty().WithMessage("Classroom capacity cannot be empty.");
            RuleFor(c => c.Capacity).GreaterThan(0).WithMessage("Classroom capacity must be greater than 0.");

            RuleFor(c => c.FacultyId).NotEmpty().WithMessage("Faculty ID cannot be empty.");
            
            RuleFor(c => c.DepartmentId).NotEmpty().WithMessage("Department ID cannot be empty.");
        }
    }
}

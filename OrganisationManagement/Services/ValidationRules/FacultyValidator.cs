using FluentValidation;
using OrganisationManagement.Model;

namespace OrganisationManagement.Services.ValidationRules
{
    public class FacultyValidator : AbstractValidator<Faculty>
    {
        public FacultyValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("Faculty name cannot be empty");
            RuleFor(f => f.Name).MinimumLength(3).WithMessage("Faculty name must be at least 3 characters");
            RuleFor(f => f.Name).Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Faculty name can only contain letters, numbers and spaces");
            RuleFor(f => f.Name).MaximumLength(50).WithMessage("Faculty name cannot be longer than 50 characters");
            
            RuleFor(f => f.Code).NotEmpty().WithMessage("Faculty code cannot be empty");
            RuleFor(f => f.Code).MinimumLength(2).WithMessage("Faculty code must be at least 2 characters");
            RuleFor(f => f.Code).Matches(@"^[a-zA-Z0-9]*$").WithMessage("Faculty code can only contain letters and numbers");
            RuleFor(f => f.Code).MaximumLength(10).WithMessage("Faculty code cannot be longer than 10 characters");
        }
    }
}

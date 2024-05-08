using FluentValidation;
using UserManagement.Models;

namespace UserManagement.Services.ValidationRules
{
    public sealed class UserDetailValidator: AbstractValidator<UserDetail>
    {
        public UserDetailValidator()
        {
            RuleFor(x => x.FacultyId).NotEmpty().WithMessage("Faculty is required.");
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("Department is required.");
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).WithMessage("First Name is invalid.");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).WithMessage("Last Name is invalid.");
        }
    }
}

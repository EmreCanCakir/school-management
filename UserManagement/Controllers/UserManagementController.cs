using AutoMapper;
using FluentValidation;
using Infrastructure.Entities.Abstracts;
using Infrastructure.Utilities.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using UserManagement.Models;
using UserManagement.Models.Dtos;
using UserManagement.Services;
using UserManagement.Services.ValidationRules;

namespace UserManagement.Controllers
{
    public class UserManagementController: ControllerBase
    {
        private static readonly EmailAddressAttribute _emailAddressAttribute = new();
        private readonly IUserDetailService _userDetailService;
        private readonly IValidator<UserDetail> _validator;
        private readonly IMapper _mapper;

        public UserManagementController(IUserDetailService userDetailService, IValidator<UserDetail> validator, IMapper mapper)
        {
            _userDetailService = userDetailService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpPost("StudentRegister")]
        public async Task<Results<Ok, ValidationProblem>> RegisterStudent(StudentRegisterDto registration, [FromServices] IServiceProvider sp)
        {
            return await RegisterUser(registration, sp);
        }

        [HttpPost("LecturerRegister")]
        public async Task<Results<Ok, ValidationProblem>> RegisterLecturer(LecturerRegisterDto registration, [FromServices] IServiceProvider sp)
        {
            return await RegisterUser(registration, sp);
        }

        private async Task<Results<Ok, ValidationProblem>> RegisterUser<T>(T registration, IServiceProvider sp) where T : UserRegisterDto
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();

            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException($"User store with email support required.");
            }

            var userStore = sp.GetRequiredService<IUserStore<User>>();
            var emailStore = (IUserEmailStore<User>)userStore;
            var phoneStore = (IUserPhoneNumberStore<User>)userStore;
            var email = registration.Email;

            if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
            {
                return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)));
            }

            var userDetail = _mapper.Map<UserDetail>(registration);
            var validationResult = _validator.Validate(userDetail);
            if (!validationResult.IsValid)
            {
                string errorMessages = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
                return CreateValidationProblem("UserDetail.NotValid", errorMessages);
            }

            var user = new User();

            await userStore.SetUserNameAsync(user, registration.Username, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            await phoneStore.SetPhoneNumberAsync(user, registration.PhoneNumber, CancellationToken.None);
            var result = await userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            userDetail.UserId = user.Id;

            await _userDetailService.Add(userDetail);

            return TypedResults.Ok();
        }


        private static ValidationProblem CreateValidationProblem(string errorCode, string errorDescription) =>
        TypedResults.ValidationProblem(new Dictionary<string, string[]> {
            { errorCode, [errorDescription] }
        });

        private static ValidationProblem CreateValidationProblem(IdentityResult result)
        {
            // We expect a single error code and description in the normal case.
            // This could be golfed with GroupBy and ToDictionary, but perf! :P
            Debug.Assert(!result.Succeeded);
            var errorDictionary = new Dictionary<string, string[]>(1);

            foreach (var error in result.Errors)
            {
                string[] newDescriptions;

                if (errorDictionary.TryGetValue(error.Code, out var descriptions))
                {
                    newDescriptions = new string[descriptions.Length + 1];
                    Array.Copy(descriptions, newDescriptions, descriptions.Length);
                    newDescriptions[descriptions.Length] = error.Description;
                }
                else
                {
                    newDescriptions = [error.Description];
                }

                errorDictionary[error.Code] = newDescriptions;
            }

            return TypedResults.ValidationProblem(errorDictionary);
        }
    }
}

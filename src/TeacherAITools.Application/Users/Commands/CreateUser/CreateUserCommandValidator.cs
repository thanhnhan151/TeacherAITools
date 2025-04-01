using FluentValidation;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;

namespace TeacherAITools.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Username is required!")
                .MustAsync(async (userName, cancellation) => await CheckExistUsernameEmail(userName, "Username"))
                .WithMessage("Username has already existed!");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required!");

            RuleFor(u => u.Fullname)
                .NotEmpty().WithMessage("Fullname is required!");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required!");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required!")
                .MustAsync(async (email, cancellation) => await CheckExistUsernameEmail(email, "Email"))
                .WithMessage("Email has already existed!");

            RuleFor(p => p.RoleId)
                .NotEmpty().WithMessage("Role is required!")
                .MustAsync(async (roleId, cancellation) => await AlreadyExistId(roleId, "RoleId"))
                .WithMessage("Role does not exist!");

            RuleFor(p => p.SchoolId)
                .NotEmpty().WithMessage("School is required!")
                .MustAsync(async (schoolId, cancellation) => await AlreadyExistId(schoolId, "SchoolId"))
                .WithMessage("School does not exist!");
        }

        private async Task<bool> AlreadyExistId(int id, string option)
        {
            switch (option.ToLower())
            {
                case "roleid":
                    var role = await _unitOfWork.Roles.GetByIdAsync(id);
                    if (role is null) return false;
                    return true;
                case "schoolid":
                    var school = await _unitOfWork.Schools.GetByIdAsync(id);
                    if (school is null) return false;
                    return true;
                default:
                    return false;
            }
        }

        private async Task<bool> CheckExistUsernameEmail(string value, string option)
        {
            switch (option)
            {
                case "Username":
                    var usernameQuery = await _unitOfWork.Users
                        .GetAsync(user => user.Username.ToLower().Equals(value.ToLower()));
                    if (usernameQuery.FirstOrDefault() is null) return true;
                    return false;
                case "Email":
                    var emailQuery = await _unitOfWork.Users
                        .GetAsync(user => user.Email.ToLower().Equals(value.ToLower()));
                    if (emailQuery.FirstOrDefault() is null) return true;
                    return false;
                default:
                    return false;
            }
        }
    }
}

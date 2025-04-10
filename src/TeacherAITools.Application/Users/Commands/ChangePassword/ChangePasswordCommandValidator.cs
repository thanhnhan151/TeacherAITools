using FluentValidation;

namespace TeacherAITools.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(u => u.NewPassword)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                //            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                //.Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                //.Matches("[0-9]").WithMessage("Password must contain at least one number.")
                //.Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
                .NotEmpty().WithMessage("Password is required!");

            RuleFor(u => u.ConfirmedPassword)
                .NotEmpty().WithMessage("Email is required!")
                .Equal(u => u.NewPassword).WithMessage("Password does not match!");
        }
    }
}

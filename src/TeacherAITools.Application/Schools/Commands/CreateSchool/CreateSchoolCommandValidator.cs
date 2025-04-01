using FluentValidation;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;

namespace TeacherAITools.Application.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommandValidator : AbstractValidator<CreateSchoolCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSchoolCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("School name is required!")
                .MustAsync(async (schoolName, cancellation) => await CheckExistSchoolName(schoolName))
                .WithMessage("Username has already existed!");

            RuleFor(s => s.Description)
                .NotEmpty().WithMessage("Description is required!");

            RuleFor(s => s.Address)
                .NotEmpty().WithMessage("Address is required!");
        }

        private async Task<bool> CheckExistSchoolName(string schoolName)
        {
            var schoolQuery = await _unitOfWork.Schools.GetAsync(
               school => school.Name.ToLower().Equals(schoolName.ToLower()));

            if (schoolQuery.FirstOrDefault() is null) return true;

            return false;
        }
    }
}

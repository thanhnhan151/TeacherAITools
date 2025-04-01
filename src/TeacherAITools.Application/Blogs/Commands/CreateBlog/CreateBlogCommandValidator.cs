using FluentValidation;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;

namespace TeacherAITools.Application.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBlogCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required!");

            RuleFor(b => b.Body)
                .NotEmpty().WithMessage("Body is required");

            RuleFor(b => b.CategoryId)
                .MustAsync(async (categoryId, cancellation) => await AlreadyExistId(categoryId))
                .WithMessage("Category does not exist!");
        }

        private async Task<bool> AlreadyExistId(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            if (category is null) return false;

            return true;
        }
    }
}

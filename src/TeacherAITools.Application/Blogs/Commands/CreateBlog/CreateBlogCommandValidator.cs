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
                .MaximumLength(150).WithMessage("Title can not have over 150 characters")
                .NotEmpty().WithMessage("Title is required!");

            RuleFor(b => b.Body)
                .NotEmpty().WithMessage("Body is required");

            RuleFor(b => b.CategoryId)
                .MustAsync(async (categoryId, cancellation) => await AlreadyExistCategoryId(categoryId))
                .WithMessage("Category does not exist!");

            RuleFor(b => b.TeacherLessonId)
                .MustAsync(async (teacherLessonId, cancellation) => await AlreadyExistLessonId(teacherLessonId))
                .WithMessage("Category does not exist!");
        }

        private async Task<bool> AlreadyExistCategoryId(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            if (category is null) return false;

            return true;
        }

        private async Task<bool> AlreadyExistLessonId(int teacherLessonId)
        {
            if (teacherLessonId == 0) return true;

            var category = await _unitOfWork.TeacherLessons.GetByIdAsync(teacherLessonId);

            if (category is null) return false;

            return true;
        }
    }
}

using FluentValidation;
using TeacherAITools.Application.Blogs.Common;

namespace TeacherAITools.Application.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogRequest>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required!");

            RuleFor(b => b.Body)
                .NotEmpty().WithMessage("Body is required");
        }
    }
}

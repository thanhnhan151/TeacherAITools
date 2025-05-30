using MediatR;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Commands.CreateBlog
{
    public record CreateBlogCommand(
        string Title,
        string Body,
        int CategoryId,
        int TeacherLessonId,
        int UserId) : IRequest<Response<GetBlogResponse>>;
}

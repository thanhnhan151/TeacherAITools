using MediatR;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Commands.UpdateBlog
{
    public record UpdateBlogCommand(
        int Id,
        UpdateBlogRequest UpdateBlogRequest) : IRequest<Response<GetBlogResponse>>;
}

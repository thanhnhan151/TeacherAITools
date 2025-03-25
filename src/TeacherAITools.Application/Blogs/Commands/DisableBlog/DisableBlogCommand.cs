using MediatR;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Commands.DisableBlog
{
    public record DisableBlogCommand(int Id) : IRequest<Response<GetBlogResponse>>;
}

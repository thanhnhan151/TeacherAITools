using MediatR;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Queries.GetBlogById
{
    public record GetBlogByIdQuery(int Id) : IRequest<Response<GetBlogDetailResponse>>;
}

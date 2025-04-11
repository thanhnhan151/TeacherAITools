using MediatR;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Queries.GetBlogs
{
    public record GetBlogsQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        bool IsActive,
        int? CategoryId,
        int Page,
        int PageSize) : IRequest<Response<PaginatedList<GetBlogResponse>>>;
}

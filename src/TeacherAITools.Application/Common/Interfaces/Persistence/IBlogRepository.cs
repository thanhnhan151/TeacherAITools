using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<PaginatedList<Blog>> PaginatedListAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int? userId
            , int? categoryId
            , bool isActive
            , int page
            , int pageSize);
    }
}

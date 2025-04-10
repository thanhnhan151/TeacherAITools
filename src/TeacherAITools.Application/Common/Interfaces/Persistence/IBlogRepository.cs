using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Common;
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
            , int? categoryId
            , bool isActive
            , LessonStatus status
            , int page
            , int pageSize);
    }
}

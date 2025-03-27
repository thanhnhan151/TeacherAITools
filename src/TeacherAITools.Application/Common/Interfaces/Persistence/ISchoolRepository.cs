using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface ISchoolRepository : IRepository<School>
    {
        Task<PaginatedList<School>> PaginatedListAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , bool isActive
            , int page
            , int pageSize);
    }
}

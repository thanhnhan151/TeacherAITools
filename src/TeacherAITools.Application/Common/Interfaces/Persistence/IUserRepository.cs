using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PaginatedList<User>> PaginatedListAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int? roleId
            , bool isActive
            , int page
            , int pageSize);
    }
}

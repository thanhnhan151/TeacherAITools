using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<PaginatedList<Quiz>> PaginatedListAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int? lessonId
            , int page
            , int pageSize);

        int GetLastIdQuiz();
    }
}

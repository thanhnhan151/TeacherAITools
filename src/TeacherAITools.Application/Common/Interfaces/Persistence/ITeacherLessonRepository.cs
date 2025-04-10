using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface ITeacherLessonRepository : IRepository<TeacherLesson>
    {
        Task<PaginatedList<TeacherLesson>> PaginatedListAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int? moduleId
            , int? lessonId
            , int? userId
            , LessonStatus status
            , int page
            , int pageSize);

        Task<bool> IsBelongedToTeacherAsync(int userId, int promptId);
    }
}

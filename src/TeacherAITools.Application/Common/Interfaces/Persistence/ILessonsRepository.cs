using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface ILessonsRepository : IRepository<Lesson>
    {
        Task<PaginatedList<Lesson>> PaginatedListAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int? lessonTypeId
            , int? moduleId
            , int? isActive
            , int page
            , int pageSize);

        int GetLastIdLesson();
    }
}
using MediatR;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessons
{
    public record GetLessonsQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int? LessonTypeId,
        int? ModuleId,
        int? IsActive,
        int Page,
        int PageSize) : IRequest<Response<PaginatedList<GetLessonResponse>>>;
}
using MediatR;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessons
{
    public record GetTeacherLessonsQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int? ModuleId,
        int? LessonId,
        int? UserId,
        LessonStatus Status,
        int Page,
        int PageSize) : IRequest<Response<PaginatedList<GetTeacherLessonResponse>>>;
}

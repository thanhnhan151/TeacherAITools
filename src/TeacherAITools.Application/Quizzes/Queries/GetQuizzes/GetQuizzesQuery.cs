using MediatR;
using TeacherAITools.Application.Quizzes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Quizzes.Queries.GetQuizzes
{
    public record GetQuizzesQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int? LessonId,
        int Page,
        int PageSize) : IRequest<Response<PaginatedList<GetQuizResponse>>>;
}

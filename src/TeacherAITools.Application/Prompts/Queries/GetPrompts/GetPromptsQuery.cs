using MediatR;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Queries.GetPrompts
{
    public record GetPromptsQuery(
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        int Page,
        int PageSize) : IRequest<Response<PaginatedList<GetPromptResponse>>>;
}

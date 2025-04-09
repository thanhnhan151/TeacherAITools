using MediatR;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Queries.GetPromptByLessonId
{
    public record GetPromptByLessonIdQuery(int LessonId) : IRequest<Response<GetPromptResponse>>;
}

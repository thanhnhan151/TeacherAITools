using MediatR;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.CreateTeacherLesson
{
    public record CreateTeacherLessonCommand(
        string StartUp,
        string KnowLedge,
        string Goal,
        string SchoolSupply,
        string Practice,
        string Apply,
        int PromptId) : IRequest<Response<GetTeacherLessonResponse>>;
}

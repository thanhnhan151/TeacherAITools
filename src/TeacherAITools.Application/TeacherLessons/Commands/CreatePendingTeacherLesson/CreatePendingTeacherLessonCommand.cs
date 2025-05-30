using MediatR;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.CreatePendingTeacherLesson
{
    public record CreatePendingTeacherLessonCommand(
        string StartUp,
        string KnowLedge,
        string Goal,
        string SchoolSupply,
        string Practice,
        string Apply,
        int UserId,
        int LessonId) : IRequest<Response<GetTeacherLessonResponse>>;
}

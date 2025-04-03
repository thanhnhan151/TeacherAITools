using MediatR;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;


namespace TeacherAITools.Application.Lessons.Commands.UpdateIsApprovedLesson
{
    public record UpdateIsApprovedLessonCommand(
        int Id,
        UpdateIsApprovedRequest updateIsApprovedRequest) : IRequest<Response<GetLessonResponse>>;
}

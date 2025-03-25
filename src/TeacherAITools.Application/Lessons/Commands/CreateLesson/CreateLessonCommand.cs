using MediatR;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.CreateLesson{
    public record CreateLessonCommand(
        CreateLessonRequest createLessonRequest) : IRequest<Response<GetLessonResponse>>;
}
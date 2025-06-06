
using MediatR;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.DeleteLesson
{
    public record DeleteLessonCommand(int Id) : IRequest<Response<GetLessonResponse>>;
}
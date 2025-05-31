using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.DeleteTeacherLesson
{
    public record DeleteTeacherLessonCommand(int Id) : IRequest<Response<string>>;
}

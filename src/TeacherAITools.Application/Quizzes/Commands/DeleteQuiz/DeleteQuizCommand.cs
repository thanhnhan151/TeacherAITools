using MediatR;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Quizzes.Commands.DeleteQuiz
{
    public record DeleteQuizCommand(int Id) : IRequest<Response<string>>;
}

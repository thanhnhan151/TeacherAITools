using MediatR;
using TeacherAITools.Application.Quizzes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Quizzes.Commands.CreateQuiz
{
    public record CreateQuizCommand(CreateQuizRequest CreateQuizRquest) : IRequest<Response<GetQuizResponse>>;
}

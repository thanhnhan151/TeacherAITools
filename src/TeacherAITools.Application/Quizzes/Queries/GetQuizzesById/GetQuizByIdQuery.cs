using MediatR;
using TeacherAITools.Application.Quizzes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Quizzes.Queries.GetQuizzesById
{
    public record GetQuizByIdQuery(int QuizId) : IRequest<Response<GetQuizDetailResponse>>;
}

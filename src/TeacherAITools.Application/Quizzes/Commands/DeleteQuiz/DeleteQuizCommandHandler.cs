using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Quizzes.Commands.DeleteQuiz
{
    public class DeleteQuizCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteQuizCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            var quizQuery = await _unitOfWork.Quizzes.GetAsync(q => q.QuizId == request.Id);

            var quiz = quizQuery
                .Include(u => u.QuizQuestions)
                        .ThenInclude(w => w.QuizAnswers)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.QUIZ_NOT_FOUND);

            foreach (var item in quiz.QuizQuestions)
            {
                await _unitOfWork.QuizAnswers.DeleteRangeAsync(item.QuizAnswers);
                await _unitOfWork.CompleteAsync();
            }

            await _unitOfWork.QuizQuestions.DeleteRangeAsync(quiz.QuizQuestions);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.Quizzes.DeleteAsync(quiz);
            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}

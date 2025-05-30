using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Quizzes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Quizzes.Queries.GetQuizzesById
{
    public class GetQuizByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetQuizByIdQuery, Response<GetQuizDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetQuizDetailResponse>> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
        {
            var quizQuery = await _unitOfWork.Quizzes.GetAsync(q => q.QuizId == request.QuizId);

            var quiz = quizQuery
                .Include(q => q.User)
                .Include(q => q.Lesson)
                        .ThenInclude(q => q.Module)
                .Include(q => q.QuizQuestions)
                        .ThenInclude(q => q.QuizAnswers)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.QUIZ_NOT_FOUND);

            return new Response<GetQuizDetailResponse>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<GetQuizDetailResponse>(quiz),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Quizzes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Quizzes.Queries.GetQuizzes
{
    public class GetQuizzesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetQuizzesQuery, Response<PaginatedList<GetQuizResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<PaginatedList<GetQuizResponse>>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
        {
            return new Response<PaginatedList<GetQuizResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<PaginatedList<GetQuizResponse>>(await _unitOfWork.Quizzes.PaginatedListAsync(
                    request.SearchTerm,
                    request.SortColumn,
                    request.SortOrder,
                    request.UserId,
                    request.LessonId,
                    request.Page,
                    request.PageSize
                )),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

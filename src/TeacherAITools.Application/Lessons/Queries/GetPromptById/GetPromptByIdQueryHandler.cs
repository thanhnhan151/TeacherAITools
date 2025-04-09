using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetPromptById
{
    public class GetPromptByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetPromptByIdQuery, Response<GetPromptResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetPromptResponse>> Handle(GetPromptByIdQuery request, CancellationToken cancellationToken)
        {
            var promptQuery = await _unitOfWork.Prompts.GetAsync(user => user.LessonId == request.Id);

            var prompt = promptQuery
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            return new Response<GetPromptResponse>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<GetPromptResponse>(prompt), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

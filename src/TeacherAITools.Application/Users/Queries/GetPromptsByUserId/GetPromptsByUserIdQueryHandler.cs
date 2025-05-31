using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetPromptsByUserId
{
    public class GetPromptsByUserIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetPromptsByUserIdQuery, Response<List<GetPromptResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetPromptResponse>>> Handle(GetPromptsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userQuery = await _unitOfWork.Users.GetAsync(user => user.UserId == request.UserId);

            var user = userQuery
                .Include(r => r.Prompts)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.USER_NOT_FOUND);

            return new Response<List<GetPromptResponse>>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<List<GetPromptResponse>>(user.Prompts), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

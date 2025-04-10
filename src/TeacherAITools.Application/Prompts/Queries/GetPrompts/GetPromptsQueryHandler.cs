using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Queries.GetPrompts
{
    public class GetPromptsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetPromptsQuery, Response<PaginatedList<GetPromptResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<PaginatedList<GetPromptResponse>>> Handle(GetPromptsQuery request, CancellationToken cancellationToken)
        {
            return new Response<PaginatedList<GetPromptResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<PaginatedList<GetPromptResponse>>(await _unitOfWork.Prompts.PaginatedListAsync(
                    request.SearchTerm,
                    request.SortColumn,
                    request.SortOrder,
                    request.Page,
                    request.PageSize
                )),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

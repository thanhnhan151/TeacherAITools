using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Weeks.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Weeks.Queries.GetWeeks
{
    public class GetWeeksQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetWeeksQuery, Response<List<GetWeekResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetWeekResponse>>> Handle(GetWeeksQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetWeekResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetWeekResponse>>(await _unitOfWork.Weeks.GetAllAsync()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

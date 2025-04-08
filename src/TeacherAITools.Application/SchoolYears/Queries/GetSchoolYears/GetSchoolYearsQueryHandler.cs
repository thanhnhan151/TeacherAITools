using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.SchoolYears.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.SchoolYears.Queries.GetSchoolYears
{
    public class GetSchoolYearsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetSchoolYearsQuery, Response<List<GetSchoolYearResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetSchoolYearResponse>>> Handle(GetSchoolYearsQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetSchoolYearResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetSchoolYearResponse>>(await _unitOfWork.SchoolYears.GetAllAsync()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

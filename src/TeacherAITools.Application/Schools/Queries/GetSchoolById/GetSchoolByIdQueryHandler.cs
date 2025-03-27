using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Queries.GetSchoolById
{
    public class GetSchoolByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetSchoolByIdQuery, Response<GetSchoolResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetSchoolResponse>> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
        {
            var schoolQuery = await _unitOfWork.Schools.GetAsync(school => school.SchoolId == request.SchoolId);

            var school = schoolQuery.Include(u => u.Ward)
                                           .ThenInclude(w => w.District)
                                                       .ThenInclude(d => d.City).FirstOrDefault() ?? throw new ApiException(ResponseCode.SCHOOL_NOT_FOUND);

            return new Response<GetSchoolResponse>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<GetSchoolResponse>(school), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

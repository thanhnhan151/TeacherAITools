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
    public class GetSchoolByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSchoolByIdQuery, Response<GetSchoolResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetSchoolResponse>> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
        {
            var schoolQuery = await _unitOfWork.Schools.GetAsync(school => school.SchoolId == request.SchoolId);

            var school = schoolQuery.Include(u => u.Ward)
                                           .ThenInclude(w => w.District)
                                                       .ThenInclude(d => d.City).FirstOrDefault() ?? throw new ApiException(ResponseCode.SCHOOL_NOT_FOUND);

            var response = new GetSchoolResponse
            {
                SchoolId = school.SchoolId,
                Name = school.Name,
                Description = school.Description,
                ImageURL = school.ImageURL,
                Address = school.Address,
                Ward = school.Ward.WardName,
                District = school.Ward.District.DistrictName,
                City = school.Ward.District.City.CityName
            };

            return new Response<GetSchoolResponse>(code: (int)ResponseCode.SUCCESS, data: response, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

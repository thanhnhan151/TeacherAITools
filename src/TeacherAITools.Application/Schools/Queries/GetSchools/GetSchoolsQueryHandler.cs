using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Queries.GetSchools
{
    public class GetSchoolsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetSchoolsQuery, Response<PaginatedList<GetSchoolResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<PaginatedList<GetSchoolResponse>>> Handle(GetSchoolsQuery request, CancellationToken cancellationToken)
        {
            return new Response<PaginatedList<GetSchoolResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<PaginatedList<GetSchoolResponse>>(await _unitOfWork.Schools.PaginatedListAsync(
                    request.SearchTerm,
                    request.SortColumn,
                    request.SortOrder,
                    request.Status,
                    request.Page,
                    request.PageSize
                )),
                message: ResponseCode.SUCCESS.GetDescription());
        }

        //public async Task<PaginationResponse<GetSchoolResponse>> Handle(GetSchoolsQuery request, CancellationToken cancellationToken)
        //{
        //    var result = await _unitOfWork.Schools.PaginationAsync(
        //        page: request.PageNumber,
        //        pageSize: request.PageSize,
        //        includeFunc: u => u.Include(u => u.Ward)
        //                                   .ThenInclude(w => w.District)
        //                                               .ThenInclude(d => d.City),
        //        orderBy: request.GetOrder(),
        //        filter: request.GetExpressions(),
        //        cancellationToken: cancellationToken);

        //    return new PaginationResponse<GetSchoolResponse>(code: (int)ResponseCode.SUCCESS,
        //        data: new PaginationData<GetSchoolResponse>()
        //        {
        //            Page = request.PageNumber,
        //            PageSize = request.PageSize,
        //            TotalSize = result.Total,
        //            TotalPage = (int?)((result?.Total + (long)request.PageSize - 1) / (long)request.PageSize) ?? 0,
        //            Items = result!.Data!.ConvertAll(school => new GetSchoolResponse()
        //            {
        //                SchoolId = school.SchoolId,
        //                Name = school.Name,
        //                Description = school.Description,
        //                ImageURL = school.ImageURL,
        //                Address = school.Address,
        //                Ward = school.Ward.WardName,
        //                District = school.Ward.District.DistrictName,
        //                City = school.Ward.District.City.CityName
        //            })
        //        },
        //        message: ResponseCode.SUCCESS.GetDescription());
        //}
    }
}

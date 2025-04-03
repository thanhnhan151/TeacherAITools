using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetUsersQuery, Response<PaginatedList<GetUserResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<PaginatedList<GetUserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return new Response<PaginatedList<GetUserResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<PaginatedList<GetUserResponse>>(await _unitOfWork.Users.PaginatedListAsync(
                    request.SearchTerm,
                    request.SortColumn,
                    request.SortOrder,
                    request.RoleId,
                    request.GradeId,
                    request.IsActive,
                    request.Page,
                    request.PageSize
                )),
                message: ResponseCode.SUCCESS.GetDescription());
        }



        #region Generic Pagination
        //public async Task<PaginationResponse<GetUserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        //{
        //    var result = await _unitOfWork.Users.PaginationAsync(
        //        page: request.PageNumber,
        //        pageSize: request.PageSize,
        //        includeFunc: u => u.Include(u => u.Role),
        //        orderBy: request.GetOrder(),
        //        filter: request.GetExpressions(),
        //        cancellationToken: cancellationToken);

        //    return new PaginationResponse<GetUserResponse>(code: (int)ResponseCode.SUCCESS,
        //        data: new PaginationData<GetUserResponse>()
        //        {
        //            Page = request.PageNumber,
        //            PageSize = request.PageSize,
        //            TotalSize = result.Total,
        //            TotalPage = (int?)((result?.Total + (long)request.PageSize - 1) / (long)request.PageSize) ?? 0,
        //            Items = result.Data.ConvertAll(user => new GetUserResponse()
        //            {
        //                UserId = user.UserId,
        //                Fullname = user.Fullname,
        //                Username = user.Username,
        //                Email = user.Email,
        //                PhoneNumber = user.PhoneNumber,
        //                DateOfBirth = user.DateOfBirth,
        //                Gender = user.Gender.GetDescription(),
        //                Role = user.Role.RoleName
        //            })
        //        },
        //        message: ResponseCode.SUCCESS.GetDescription());
        //}
        #endregion
    }

    #region No pagination
    //public class GetUsersQueryHandler(
    //    IUnitOfWork unitOfWork) : IRequestHandler<GetUsersQuery, Response<List<GetUserResponse>>>
    //{
    //    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    //    public async Task<Response<List<GetUserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    //    {
    //        var result = new List<GetUserResponse>();

    //        var userQuery = await _unitOfWork.Users.GetAllAsync(includeFunc: user => user.Include(u => u.Role));

    //        var users = userQuery.ToList();

    //        foreach (var user in users)
    //        {
    //            result.Add(new GetUserResponse
    //            {
    //                Id = user.UserId,
    //                Fullname = user.Fullname,
    //                Username = user.Username,
    //                Email = user.Email,
    //                PhoneNumber = user.PhoneNumber,
    //                DateOfBirth = user.DateOfBirth,
    //                Gender = user.Gender.GetDescription(),
    //                Role = user.Role.RoleName
    //            });
    //        }

    //        return new Response<List<GetUserResponse>>(code: (int)ResponseCode.SUCCESS, data: result, message: ResponseCode.SUCCESS.GetDescription());
    //    }
    //}
    #endregion
}

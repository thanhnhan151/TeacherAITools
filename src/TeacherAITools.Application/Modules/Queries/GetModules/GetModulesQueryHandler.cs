using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Queries.GetModules
{
    public class GetModulesQueryHandler : IRequestHandler<GetModulesQuery, PaginationResponse<GetModuleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetModulesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResponse<GetModuleResponse>> Handle(GetModulesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Modules.PaginationAsync(
                page: request.PageNumber,
                pageSize: request.PageSize,
                includeFunc: m => m.Include(m => m.Curriculum)
                                   .Include(m => m.Book),
                orderBy: request.GetOrder(),
                filter: request.GetExpressions(),
                cancellationToken: cancellationToken);

            return new PaginationResponse<GetModuleResponse>(code: (int)ResponseCode.SUCCESS,
                data: new PaginationData<GetModuleResponse>()
                {
                    Page = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalSize = result.Total,
                    TotalPage = (int?)((result?.Total + (long)request.PageSize - 1) / (long)request.PageSize) ?? 0,
                    Items = result!.Data!.ConvertAll(module => new GetModuleResponse()
                    {
                        ModuleId = module.ModuleId,
                        Name = module.Name,
                        Desciption = module.Desciption,
                        Semester = module.Semester,
                        TotalPeriods = module.TotalPeriods,
                        Curriculum = module.Curriculum.Name,
                        Book = module.Book.BookName
                    })
                },
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
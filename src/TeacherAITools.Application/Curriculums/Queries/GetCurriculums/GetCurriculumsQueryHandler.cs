using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Queries.GetCurriculums
{
    public class GetCurriculumsQueryHandler : IRequestHandler<GetCurriculumsQuery, PaginationResponse<GetCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCurriculumsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResponse<GetCurriculumResponse>> Handle(GetCurriculumsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Curriculums.PaginationAsync(
                page: request.PageNumber,
                pageSize: request.PageSize,
                includeFunc: m => m.Include(m => m.SchoolYear),
                orderBy: request.GetOrder(),
                filter: request.GetExpressions(),
                cancellationToken: cancellationToken);

            return new PaginationResponse<GetCurriculumResponse>(code: (int)ResponseCode.SUCCESS,
                data: new PaginationData<GetCurriculumResponse>()
                {
                    Page = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalSize = result.Total,
                    TotalPage = (int?)((result?.Total + (long)request.PageSize - 1) / (long)request.PageSize) ?? 0,
                    Items = result!.Data!.ConvertAll(curriculum => new GetCurriculumResponse()
                    {
                        CurriculumId = curriculum.CurriculumId,
                        Name = $"{curriculum.Name} {curriculum.GradeId}",
                        Description = curriculum.Description,
                        TotalPeriods = curriculum.TotalPeriods,
                        Year = curriculum.SchoolYear.Year
                    })
                },
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
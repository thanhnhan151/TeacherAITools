using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.LessonHistories.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.LessonHistories.Queries.GetLessonHistories
{
    public class GetLessonHistoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLessonHistoriesQuery, PaginationResponse<GetLessonHistoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PaginationResponse<GetLessonHistoryResponse>> Handle(GetLessonHistoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.LessonHistories.PaginationAsync(
                page: request.PageNumber,
                pageSize: request.PageSize,
                includeFunc: m => m.Include(m => m.TeacherLesson),
                orderBy: request.GetOrder(),
                filter: request.GetExpressions(),
                cancellationToken: cancellationToken);

            return new PaginationResponse<GetLessonHistoryResponse>(code: (int)ResponseCode.SUCCESS,
                data: new PaginationData<GetLessonHistoryResponse>()
                {
                    Page = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalSize = result.Total,
                    TotalPage = (int?)((result?.Total + (long)request.PageSize - 1) / (long)request.PageSize) ?? 0,
                    Items = result!.Data!.ConvertAll(lessonHistory => new GetLessonHistoryResponse()
                    {
                        Id = lessonHistory.Id,
                        StartUp = lessonHistory.StartUp,
                        Knowledge = lessonHistory.Knowledge,
                        Practice = lessonHistory.Practice,
                        Apply = lessonHistory.Apply,
                        Goal = lessonHistory.Goal,
                        SchoolSupply = lessonHistory.SchoolSupply,
                    })
                },
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
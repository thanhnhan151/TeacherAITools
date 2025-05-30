using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessons
{
    public class GetLessonsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLessonsQuery, PaginationResponse<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PaginationResponse<GetLessonResponse>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Lessons.PaginationAsync(
                page: request.PageNumber,
                pageSize: request.PageSize,
                includeFunc: m => m.Include(m => m.LessonType)
                                   .Include(m => m.Note)
                                   .Include(m => m.Module),
                orderBy: request.GetOrder(),
                filter: request.GetExpressions(),
                cancellationToken: cancellationToken);

            return new PaginationResponse<GetLessonResponse>(code: (int)ResponseCode.SUCCESS,
                data: new PaginationData<GetLessonResponse>()
                {
                    Page = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalSize = result.Total,
                    TotalPage = (int?)((result?.Total + (long)request.PageSize - 1) / (long)request.PageSize) ?? 0,
                    Items = result!.Data!.ConvertAll(lesson => new GetLessonResponse()
                    {
                        LessonId = lesson.LessonId,
                        Name = lesson.Name,
                        Description = lesson.Description,
                        TotalPeriods = lesson.TotalPeriods,
                        LessonType = lesson.LessonType.LessonTypeName,
                        Note = lesson.Note.Description,
                        Module = lesson.Module.Name,
                        GradeNumber = lesson.Module.Grade.GradeId,
                    })
                },
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

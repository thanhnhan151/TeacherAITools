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
                                   //.Include(m => m.Requirement)
                                   //.Include(m => m.SchoolSupply)
                                   .Include(m => m.Note)
                                   //.Include(m => m.User)
                                   .Include(m => m.Week)
                                   .Include(m => m.Module)
                                           .ThenInclude(m => m.Grade),
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
                        //Requirement = lesson.Requirement.Description,
                        //SchoolSupply = lesson.SchoolSupply.Description,
                        Note = lesson.Note.Description,
                        //User = lesson.User.Username,
                        Week = lesson.Week.WeekNumber,
                        Module = lesson.Module.Name,
                        GradeNumber = lesson.Module.Grade.GradeNumber,
                    })
                },
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

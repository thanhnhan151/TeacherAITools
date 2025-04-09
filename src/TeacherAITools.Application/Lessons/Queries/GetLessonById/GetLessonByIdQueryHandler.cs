using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessonById
{
    public class GetLessonByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLessonByIdQuery, Response<GetLessonDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetLessonDetailResponse>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(lesson => lesson.LessonId == request.id);

            var lesson = lessonQuery
                .Include(l => l.LessonType)
                //.Include(l => l.Requirement)
                //.Include(l => l.SchoolSupply)
                .Include(l => l.Note)
                //.Include(l => l.User)
                .Include(l => l.Week)
                .Include(l => l.Module)
                        .ThenInclude(m => m.Grade)
                //.Include(l => l.Periods)
                //        .ThenInclude(p => p.PeriodDetails)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            var response = new GetLessonDetailResponse
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
                //Period = lesson.Periods.ToList().ConvertAll(period => new PeriodResponse
                //{
                //    Id = period.Id,
                //    Number = period.Number,
                //    PeriodDetails = period.PeriodDetails.ToList().ConvertAll(detail => new GetPeriodDetail
                //    {
                //        Id = detail.Id,
                //        StartUp = detail.StartUp,
                //        Knowledge = detail.Knowledge,
                //        Practice = detail.Practice,
                //        Apply = detail.Apply
                //    })
                //})
            };

            return new Response<GetLessonDetailResponse>(code: (int)ResponseCode.SUCCESS, data: response, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

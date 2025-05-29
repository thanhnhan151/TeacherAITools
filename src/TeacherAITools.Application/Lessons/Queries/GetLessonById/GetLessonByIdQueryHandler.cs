using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Models.Responses;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Queries.GetLessonById
{
    public class GetLessonByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetLessonByIdQuery, Response<GetLessonDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetLessonDetailResponse>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(lesson => lesson.LessonId == request.id);

            var lesson = lessonQuery
                .Include(l => l.StartUp)
                .Include(l => l.KnowLedge)
                .Include(l => l.Practice)
                .Include(l => l.Apply)
                .Include(l => l.LessonType)
                .Include(l => l.Note)
                .Include(l => l.Week)
                .Include(l => l.Module)
                        .ThenInclude(m => m.Grade)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            var response = new GetLessonDetailResponse
            {
                LessonId = lesson.LessonId,
                Name = lesson.Name,
                Description = lesson.Description,
                TotalPeriods = lesson.TotalPeriods,
                LessonType = lesson.LessonType.LessonTypeName,
                Note = lesson.Note.Description,
                Week = lesson.Week.WeekNumber,
                Module = lesson.Module.Name,
                GradeNumber = lesson.Module.Grade.GradeNumber,
                SpecialAbility = lesson.SpecialAbility,
                GeneralCapacity = lesson.GeneralCapacity,
                Quality = lesson.Quality,
                Duration = lesson.Duration,
                StartUp = new StartUpResponse
                {
                    StartUpId = lesson.StartUp.StartUpId,
                    Goal = lesson.StartUp.Goal,
                    TeacherActivities = lesson.StartUp.TeacherActivities,
                    StudentActivities = lesson.StartUp.StudentActivities,
                    Duration = lesson.StartUp.Duration,
                },
                KnowLedge = new KnowLedgeResponse
                {
                    KnowLedgeId = lesson.KnowLedge.KnowLedgeId,
                    Goal = lesson.KnowLedge.Goal,
                    TeacherActivities = lesson.KnowLedge.TeacherActivities,
                    StudentActivities = lesson.KnowLedge.StudentActivities,
                    Duration = lesson.KnowLedge.Duration,
                },
                Practice = new PracticeResponse
                {
                    PracticeId = lesson.Practice.PracticeId,
                    Goal = lesson.Practice.Goal,
                    TeacherActivities = lesson.Practice.TeacherActivities,
                    StudentActivities = lesson.Practice.StudentActivities,
                    Duration = lesson.Practice.Duration,
                },
                Apply = new ApplyResponse
                {
                    ApplyId = lesson.Apply.ApplyId,
                    Goal = lesson.Apply.Goal,
                    TeacherActivities = lesson.Apply.TeacherActivities,
                    StudentActivities = lesson.Apply.StudentActivities,
                    Duration = lesson.Apply.Duration,
                }
            };

            return new Response<GetLessonDetailResponse>(code: (int)ResponseCode.SUCCESS, data: response, message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

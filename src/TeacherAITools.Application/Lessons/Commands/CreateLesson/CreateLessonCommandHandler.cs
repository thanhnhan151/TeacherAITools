using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Response<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetLessonResponse>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = new Lesson
            {
                Name = request.createLessonRequest.Name,
                Description = request.createLessonRequest.Description,
                TotalPeriods = request.createLessonRequest.TotalPeriods,
                LessonTypeId = request.createLessonRequest.LessonTypeId,
                RequirementId = request.createLessonRequest.RequirementId,
                TeachingToolId = request.createLessonRequest.TeachingToolId,
                NoteId = request.createLessonRequest.NoteId,
                UserId = request.createLessonRequest.UserId,
                WeekId = request.createLessonRequest.WeekId,
                ModuleId = request.createLessonRequest.ModuleId
            };

            var result = await _unitOfWork.Lessons.AddAsync(lesson);

            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

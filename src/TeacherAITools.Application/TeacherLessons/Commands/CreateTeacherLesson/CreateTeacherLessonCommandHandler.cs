using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.CreateTeacherLesson
{
    public class CreateTeacherLessonCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<CreateTeacherLessonCommand, Response<GetTeacherLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetTeacherLessonResponse>> Handle(CreateTeacherLessonCommand request, CancellationToken cancellationToken)
        {
            TeacherLesson teacherLesson = new()
            {
                StartUp = request.StartUp,
                Knowledge = request.KnowLedge,
                Practice = request.Practice,
                Apply = request.Apply,
                Goal = request.Goal,
                SchoolSupply = request.SchoolSupply,
                UserId = 2,
                PromptId = request.PromptId
            };

            await _unitOfWork.TeacherLessons.AddAsync(teacherLesson);

            await _unitOfWork.CompleteAsync();

            return new Response<GetTeacherLessonResponse>(code: (int)ResponseCode.CREATED_SUCCESS,
                message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

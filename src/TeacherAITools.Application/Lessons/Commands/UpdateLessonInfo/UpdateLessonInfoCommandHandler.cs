using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateLessonInfo
{
    public class UpdateLessonInfoCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateLessonInfoCommand, Response<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetLessonResponse>> Handle(UpdateLessonInfoCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = [];

            var lessonQuery = await _unitOfWork.Lessons.GetAsync(expression: u => u.LessonId == request.LessonId, disableTracking: true);

            var lesson = lessonQuery.FirstOrDefault();

            if (lesson is null)
            {
                errorMessages.Add(ResponseCode.USER_NOT_FOUND.GetDescription());
                throw new ValidationException(ResponseCode.USER_NOT_FOUND, errorMessages);
            }

            lesson.SpecialAbility = request.SpecialAbility;
            lesson.GeneralCapacity = request.GeneralCapacity;
            lesson.Quality = request.Quality;
            lesson.SchoolSupply = request.SchoolSupply;
            lesson.Duration = "35";

            await _unitOfWork.Lessons.UpdateAsync(lesson);
            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateLessonCommand, Response<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetLessonResponse>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            if (!_unitOfWork.LessonTypes.Any(
                x => x.LessonTypeId == request.updateLessonRequest.LessonTypeId))
            {
                throw new ApiException(ResponseCode.ID_LESSON_TYPE_DONT_EXIST);
            }

            //if (!_unitOfWork.Requirements.Any(
            //    x => x.RequirementId == request.updateLessonRequest.RequirementId))
            //{
            //    throw new ApiException(ResponseCode.ID_REQUIREMENT_DONT_EXIST);
            //}

            if (!_unitOfWork.Notes.Any(
                x => x.NoteId == request.updateLessonRequest.NoteId))
            {
                throw new ApiException(ResponseCode.ID_NOTE_DONT_EXIST);
            }

            //if (!_unitOfWork.SchoolSupplies.Any(
            //    x => x.SchoolSupplyId == request.updateLessonRequest.SchoolSupplyId))
            //{
            //    throw new ApiException(ResponseCode.ID_WEEK_DONT_EXIST);
            //}

            //if (!_unitOfWork.SchoolSupplies.Any(
            //    x => x.SchoolSupplyId == request.updateLessonRequest.SchoolSupplyId))
            //{
            //    throw new ApiException(ResponseCode.ID_WEEK_DONT_EXIST);
            //}

            //if (!_unitOfWork.Users.Any(
            //    x => x.UserId == request.updateLessonRequest.UserId))
            //{
            //    throw new ApiException(ResponseCode.USER_NOT_FOUND);
            //}

            //if (!_unitOfWork.Modules.Any(
            //    x => x.ModuleId == request.updateLessonRequest.ModuleId))
            //{
            //    throw new ApiException(ResponseCode.MODULE_NOT_FOUND);
            //}

            var lessonQuery = await _unitOfWork.Lessons.GetAsync(
                expression: m => m.LessonId == request.Id,
                includeFunc: m => m.Include(m => m.Module)
                                   .ThenInclude(m => m.Curriculum),
                disableTracking: true);

            var lesson = lessonQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            if (request.updateLessonRequest.TotalPeriods > lesson.TotalPeriods)
            {
                lesson.TotalPeriods += 1;
                lesson.Module.TotalPeriods += 1;
                lesson.Module.Curriculum.TotalPeriods += 1;
            }

            if (request.updateLessonRequest.TotalPeriods < lesson.TotalPeriods)
            {
                lesson.TotalPeriods -= 1;
                lesson.Module.TotalPeriods -= 1;
                lesson.Module.Curriculum.TotalPeriods -= 1;
            }

            lesson.Name = request.updateLessonRequest.Name;
            lesson.Description = request.updateLessonRequest.Description;
            lesson.LessonTypeId = request.updateLessonRequest.LessonTypeId;
            lesson.NoteId = request.updateLessonRequest.NoteId;

            await _unitOfWork.Lessons.UpdateAsync(lesson);

            await _unitOfWork.Modules.UpdateAsync(lesson.Module);

            await _unitOfWork.Curriculums.UpdateAsync(lesson.Module.Curriculum);
            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}

using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateLessonCommand, Response<GetLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetLessonResponse>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            if (!_unitOfWork.LessonTypes.Any(
                x => x.LessonTypeId == request.createLessonRequest.LessonTypeId))
            {
                throw new ApiException(ResponseCode.ID_LESSON_TYPE_DONT_EXIST);
            }

            //if (!_unitOfWork.Requirements.Any(
            //    x => x.RequirementId == request.createLessonRequest.RequirementId))
            //{
            //    throw new ApiException(ResponseCode.ID_REQUIREMENT_DONT_EXIST);
            //}

            if (!_unitOfWork.Notes.Any(
                x => x.NoteId == request.createLessonRequest.NoteId))
            {
                throw new ApiException(ResponseCode.ID_NOTE_DONT_EXIST);
            }

            //if (!_unitOfWork.SchoolSupplies.Any(
            //    x => x.SchoolSupplyId == request.createLessonRequest.SchoolSupplyId))
            //{
            //    throw new ApiException(ResponseCode.ID_WEEK_DONT_EXIST);
            //}

            //if (!_unitOfWork.SchoolSupplies.Any(
            //    x => x.SchoolSupplyId == request.createLessonRequest.SchoolSupplyId))
            //{
            //    throw new ApiException(ResponseCode.ID_WEEK_DONT_EXIST);
            //}

            //if (!_unitOfWork.Users.Any(
            //    x => x.UserId == request.createLessonRequest.UserId))
            //{
            //    throw new ApiException(ResponseCode.USER_NOT_FOUND);
            //}

            if (!_unitOfWork.Modules.Any(
                x => x.ModuleId == request.createLessonRequest.ModuleId))
            {
                throw new ApiException(ResponseCode.MODULE_NOT_FOUND);
            }

            var lessonId = _unitOfWork.Lessons.GetLastIdLesson() + 1;

            var lesson = new Lesson
            {
                LessonId = lessonId,
                Name = request.createLessonRequest.Name,
                Description = request.createLessonRequest.Description,
                TotalPeriods = request.createLessonRequest.TotalPeriods,
                LessonTypeId = request.createLessonRequest.LessonTypeId,
                //RequirementId = request.createLessonRequest.RequirementId,
                //SchoolSupplyId = request.createLessonRequest.SchoolSupplyId,
                NoteId = request.createLessonRequest.NoteId,
                //UserId = request.createLessonRequest.UserId,
                ModuleId = request.createLessonRequest.ModuleId
            };

            var result = await _unitOfWork.Lessons.AddAsync(lesson);

            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

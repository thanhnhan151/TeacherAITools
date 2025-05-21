using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.DeleteCurriculum
{
    public class DeleteCurriculumCommandHandler : IRequestHandler<DeleteCurriculumCommand, Response<GetCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCurriculumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetCurriculumResponse>> Handle(DeleteCurriculumCommand request, CancellationToken cancellationToken)
        {
            var curriculumQuery = await _unitOfWork.Curriculums.GetAsync(expression: m => m.CurriculumId == request.Id, disableTracking: true);

            var curriculum = curriculumQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);

            //if (curriculum.IsActive)
            //{
            //    curriculum.IsActive = false;
            //}
            //else
            //{
            //    curriculum.IsActive = true;
            //}

            await _unitOfWork.Curriculums.DeleteAsync(curriculum);
            await _unitOfWork.CompleteAsync();

            return new Response<GetCurriculumResponse>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}
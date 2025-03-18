using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.DeleteCurriculum
{
    public class DeleteCurriculumCommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCurriculumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetCurriculumResponse>> Handle(int request, CancellationToken cancellationToken)
        {
            var curriculumQuery = await _unitOfWork.Curriculums.GetAsync(expression: m => m.CurriculumId == request, disableTracking: true);

            var curriculum = curriculumQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);

            await _unitOfWork.Curriculums.UpdateAsync(curriculum);
            await _unitOfWork.CompleteAsync();

            return new Response<GetCurriculumResponse>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}
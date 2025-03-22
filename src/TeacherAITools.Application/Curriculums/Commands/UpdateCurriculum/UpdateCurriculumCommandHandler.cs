using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.UpdateCurriculum
{
    public class UpdateCurriculumCommandHandler : IRequestHandler<UpdateCurriculumCommand, Response<GetCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCurriculumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetCurriculumResponse>> Handle(UpdateCurriculumCommand request, CancellationToken cancellationToken)
        {
            var curriculumQuery = await _unitOfWork.Curriculums.GetAsync(expression: m => m.CurriculumId == request.Id, disableTracking: true);

            var curriculum = curriculumQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);

            curriculum.Name = request.updateCurriculumRequest.Name;
            curriculum.Description = request.updateCurriculumRequest.Description;
            curriculum.TotalPeriods = request.updateCurriculumRequest.TotalPeriods;
            //curriculum.GradeId = request.updateCurriculumRequest.GradeId;
            curriculum.SchoolYearId = request.updateCurriculumRequest.SchoolYearId;

            await _unitOfWork.Curriculums.UpdateAsync(curriculum);
            await _unitOfWork.CompleteAsync();

            return new Response<GetCurriculumResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}

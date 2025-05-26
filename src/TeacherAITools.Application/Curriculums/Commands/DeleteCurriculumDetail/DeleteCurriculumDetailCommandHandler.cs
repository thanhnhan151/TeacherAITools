using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.DeleteCurriculumDetail
{
    public class DeleteCurriculumDetailCommandHandler : IRequestHandler<DeleteCurriculumDetailCommand, Response<GetDetailCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCurriculumDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetDetailCurriculumResponse>> Handle(DeleteCurriculumDetailCommand request, CancellationToken cancellationToken)
        {
            var curriculumDetailQuery = await _unitOfWork.CurriculumDetails.GetAsync(expression: m => m.CurriculumDetailId == request.Id, disableTracking: true);

            var curriculumDetail = curriculumDetailQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.CURRICULUM_DETAIL_NOT_FOUND);

            await _unitOfWork.CurriculumDetails.DeleteAsync(curriculumDetail);
            await _unitOfWork.CompleteAsync();

            return new Response<GetDetailCurriculumResponse>(code: (int)ResponseCode.DELETED_SUCCESS, message: ResponseCode.DELETED_SUCCESS.GetDescription());
        }
    }
}
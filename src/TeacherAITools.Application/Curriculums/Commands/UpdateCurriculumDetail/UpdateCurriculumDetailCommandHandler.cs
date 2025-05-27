using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.UpdateCurriculumDetail
{
    public class UpdateCurriculumDetailCommandHandler 
    : IRequestHandler<UpdateCurriculumDetailCommand, Response<GetDetailCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCurriculumDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetDetailCurriculumResponse>> Handle(UpdateCurriculumDetailCommand request, CancellationToken cancellationToken)
        {
            var curriculumDetailQuery = await _unitOfWork.CurriculumDetails
            .GetAsync(curriculum => curriculum.CurriculumDetailId == request.Id);

            var curriculumDetail = curriculumDetailQuery
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);

            curriculumDetail.CurriculumContent = request.updateCurriculumRequest.CurriculumContent;
            curriculumDetail.CurriculumGoal = request.updateCurriculumRequest.CurriculumGoal;
            curriculumDetail.CurriculumId = request.updateCurriculumRequest.CurriculumId;
            curriculumDetail.CurriculumSubSectionId = request.updateCurriculumRequest.CurriculumSubSectionId;

            await _unitOfWork.CurriculumDetails.UpdateAsync(curriculumDetail);
            await _unitOfWork.CompleteAsync();

            return new Response<GetDetailCurriculumResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.CreateCurriculumDetail
{
    public class CreateCurriculumDetailCommandHandler 
    : IRequestHandler<CreateCurriculumDetailCommand, Response<GetDetailCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCurriculumDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetDetailCurriculumResponse>>
        Handle(CreateCurriculumDetailCommand request, CancellationToken cancellationToken)
        {
            var curriculumDetailQuery = await _unitOfWork.CurriculumDetails
            .GetAllAsync();
            var curriculumLast = curriculumDetailQuery.OrderBy(s => s.CurriculumDetailId).LastOrDefault();

            var curriculumDetail = new CurriculumDetail
            {
                CurriculumDetailId = curriculumLast!.CurriculumDetailId + 1,
                CurriculumContent = request.createCurriculumDetailRequest.CurriculumContent,
                CurriculumGoal = request.createCurriculumDetailRequest.CurriculumGoal,
                CurriculumId = request.createCurriculumDetailRequest.CurriculumId,
                CurriculumSubSectionId = request.createCurriculumDetailRequest.CurriculumSubSectionId,
            };

            var result = await _unitOfWork.CurriculumDetails.AddAsync(curriculumDetail);

            await _unitOfWork.CompleteAsync();

            return new Response<GetDetailCurriculumResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
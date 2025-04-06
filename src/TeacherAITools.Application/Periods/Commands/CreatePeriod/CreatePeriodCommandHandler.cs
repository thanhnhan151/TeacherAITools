using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Periods.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Periods.Commands.CreatePeriod
{
    public class CreatePeriodCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePeriodCommand, Response<GetPeriodResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetPeriodResponse>> Handle(CreatePeriodCommand request, CancellationToken cancellationToken)
        {
            if(!_unitOfWork.Lessons.Any(
                x => x.LessonId == request.createPeriodRequest.LessonId)){
                    throw new ApiException(ResponseCode.LESSON_NOT_FOUND);
            }
            
            var periodId = _unitOfWork.Periods.GetLastIdPeriod() + 1;

            var period = new Period{
                Id = periodId,
                Number = request.createPeriodRequest.Number,
                LessonId = request.createPeriodRequest.LessonId,
            };

            var periodDetail = new PeriodDetail{
                StartUp = request.createPeriodRequest.StartUp,
                Knowledge = request.createPeriodRequest.Knowledge,
                Practice = request.createPeriodRequest.Practice,
                Apply = request.createPeriodRequest.Apply,
                PeriodId = periodId
            };

            await _unitOfWork.PeriodDetails.AddAsync(periodDetail);
            await _unitOfWork.Periods.AddAsync(period);

            await _unitOfWork.CompleteAsync();

            return new Response<GetPeriodResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
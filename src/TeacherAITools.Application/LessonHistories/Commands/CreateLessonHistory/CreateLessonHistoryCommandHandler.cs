using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.LessonHistories.Common;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.LessonHistories.Commands.CreateLessonHistory
{
    public class CreateLessonHistoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateLessonHistoryCommand, Response<GetLessonHistoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetLessonHistoryResponse>> Handle(CreateLessonHistoryCommand request, CancellationToken cancellationToken)
        {
            var lessonHistory = new LessonHistory
            {
                StartUp = request.createLessonHistoryRequest.StartUp,
                Knowledge = request.createLessonHistoryRequest.Knowledge,
                Practice = request.createLessonHistoryRequest.Practice,
                Apply = request.createLessonHistoryRequest.Apply,
                Goal = request.createLessonHistoryRequest.Goal,
                SchoolSupply = request.createLessonHistoryRequest.SchoolSupply,
            };

            var result = await _unitOfWork.LessonHistories.AddAsync(lessonHistory);

            await _unitOfWork.CompleteAsync();

            return new Response<GetLessonHistoryResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}
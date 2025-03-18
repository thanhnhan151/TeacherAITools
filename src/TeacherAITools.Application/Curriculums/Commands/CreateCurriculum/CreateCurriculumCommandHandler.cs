using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.CreateCurriculum
{
    public class CreateCurriculumCommandHandler : IRequestHandler<CreateCurriculumCommand, Response<GetCurriculumResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCurriculumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetCurriculumResponse>> Handle(CreateCurriculumCommand request, CancellationToken cancellationToken)
        {
            var curriculum = new Curriculum
            {
                Name = request.createCurriculumRequest.Name,
                Description = request.createCurriculumRequest.Description,
                TotalPeriods = request.createCurriculumRequest.TotalPeriods,
                GradeId = request.createCurriculumRequest.GradeId,
                SchoolYearId = request.createCurriculumRequest.SchoolYearId
            };

            var result = await _unitOfWork.Curriculums.AddAsync(curriculum);

            await _unitOfWork.CompleteAsync();

            return new Response<GetCurriculumResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

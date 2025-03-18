using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Modules.Commands.CreateModule
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, Response<GetModuleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateModuleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetModuleResponse>> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            var moduleQuery = await _unitOfWork.Modules.GetAsync(
                module => module.Name.ToLower().Equals(request.createModuleRequest.Name.ToLower()));

            if (moduleQuery.FirstOrDefault() is not null) throw new ApiException(ResponseCode.MODULE_ALREADY_EXISTS);

            var module = new Module
            {
                Name = request.createModuleRequest.Name,
                Desciption = request.createModuleRequest.Desciption,
                Semester = request.createModuleRequest.Semester,
                TotalPeriods = request.createModuleRequest.TotalPeriods,
                CurriculumId = request.createModuleRequest.CurriculumId
            };

            var result = await _unitOfWork.Modules.AddAsync(module);

            await _unitOfWork.CompleteAsync();

            return new Response<GetModuleResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

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
            if (!_unitOfWork.Curriculums.Any(
                x => x.CurriculumId == request.createModuleRequest.CurriculumId))
            {
                throw new ApiException(ResponseCode.CURRICULUM_NOT_FOUND);
            }

            if (!_unitOfWork.Grades.Any(
                x => x.GradeId == request.createModuleRequest.GradeId))
            {
                throw new ApiException(ResponseCode.ID_GRADE_DONT_EXIST);
            }

            if (!_unitOfWork.Books.Any(
                x => x.BookId == request.createModuleRequest.BookId))
            {
                throw new ApiException(ResponseCode.ID_BOOK_DONT_EXIST);
            }

            var moduleQuery = await _unitOfWork.Modules.GetAsync(
                module => module.Name.ToLower().Equals(request.createModuleRequest.Name.ToLower()));

            if (moduleQuery.FirstOrDefault() is not null) throw new ApiException(ResponseCode.MODULE_ALREADY_EXISTS);

            var module = new Module
            {
                Name = request.createModuleRequest.Name,
                Desciption = request.createModuleRequest.Desciption,
                Semester = request.createModuleRequest.Semester,
                CurriculumId = request.createModuleRequest.CurriculumId,
                GradeId = request.createModuleRequest.GradeId,
                BookId = request.createModuleRequest.BookId
            };

            var result = await _unitOfWork.Modules.AddAsync(module);

            await _unitOfWork.CompleteAsync();

            return new Response<GetModuleResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

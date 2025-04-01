using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateSchoolCommand, Response<GetSchoolResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetSchoolResponse>> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSchoolCommandValidator(_unitOfWork);
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                var errorMessages = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                throw new ValidationException(ResponseCode.CREATED_UNSUCC, errorMessages);
            }

            var newSchool = new School
            {
                Name = request.Name,
                Description = request.Description,
                ImageURL = request.ImageURL,
                Address = request.Address,
                WardId = request.WardId
            };

            var res = await _unitOfWork.Schools.AddAsync(newSchool);

            await _unitOfWork.CompleteAsync();

            return new Response<GetSchoolResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

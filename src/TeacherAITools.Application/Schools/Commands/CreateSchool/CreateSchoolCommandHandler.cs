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
            var schoolQuery = await _unitOfWork.Schools.GetAsync(
               school => school.Name.Equals(request.Name));

            if (schoolQuery.FirstOrDefault() is not null) throw new ApiException(ResponseCode.SCHOOL_NAME_ERR);

            var newSchool = new School
            {
                Name = request.Name,
                Description = request.Description,
                ImageURL = request.ImageURL,
                Address = request.Address,
                WardId = request.WardId
            };

            var result = await _unitOfWork.Schools.AddAsync(newSchool);

            await _unitOfWork.CompleteAsync();

            return new Response<GetSchoolResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

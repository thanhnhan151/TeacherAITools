using MediatR;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Commands.DisableSchool
{
    public record DisableSchoolCommand(int Id) : IRequest<Response<GetSchoolResponse>>;
}

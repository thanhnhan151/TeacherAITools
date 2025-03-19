using MediatR;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Schools.Commands.CreateSchool
{
    public record CreateSchoolCommand(
        string Name,
        string Description,
        string ImageURL,
        string Address,
        int WardId) : IRequest<Response<GetSchoolResponse>>;
}

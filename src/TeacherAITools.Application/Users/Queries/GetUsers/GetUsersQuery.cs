using MediatR;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery : IRequest<Response<List<GetUserResponse>>>;
}

using MediatR;
using Microsoft.AspNetCore.Http;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Users.Commands.UploadProfileImg
{
    public record UploadProfileImgCommand(IFormFile File) : IRequest<Response<GetUserResponse>>;
}

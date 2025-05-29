using TeacherAITools.Domain.Common;

namespace TeacherAITools.Application.Users.Common
{
    public record UpdateUserRequest(
    string Fullname,
    string Email,
    string PhoneNumber,
    DateOnly DateOfBirth,
    Gender Gender,
    string Address);
}

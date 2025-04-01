namespace TeacherAITools.Application.Users.Common
{
    public record UpdateUserRequest(
        string Fullname,
        string Email,
        string PhoneNumber,
        string Address);
}

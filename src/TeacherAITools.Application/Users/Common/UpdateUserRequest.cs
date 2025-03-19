namespace TeacherAITools.Application.Users.Common
{
    public record UpdateUserRequest(
        string Fullname,
        string Email,
        string PhoneNumber,
        DateOnly? DateOfBirth,
        string ImgURL,
        string Address);
}

namespace TeacherAITools.Application.Users.Common
{
    public class UpdateUserRequest
    {
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public string ImgURL { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

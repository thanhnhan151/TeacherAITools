﻿using TeacherAITools.Domain.Common;

namespace TeacherAITools.Application.Users.Common
{
    public class GetUserUpdateResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string ImgURL { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string School { get; set; } = string.Empty;
        public string Manager { get; set; } = "N/A";
        public int WardId { get; set; }
    }
}

using TeacherAITools.Domain.Common;
using TeacherAITools.Domain.Entities.Base.Implementations;

namespace TeacherAITools.Domain.Entities
{
    public class User : AuditableEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; } = new DateOnly(2000, 1, 1);
        public Gender Gender { get; set; }
        public string? ImgURL { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? ResetPasswordOtp { get; set; }

        // Foreign Key
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        public int? ManagerId { get; set; }
        public virtual User Manager { get; set; } = null!;

        public int? SchoolId { get; set; }
        public virtual School School { get; set; } = null!;

        public int? WardId { get; set; }
        public virtual Ward Ward { get; set; } = null!;

        public int? GradeId { get; set; }
        public virtual Grade Grade { get; set; } = null!;

        // Navigation
        //public virtual ICollection<Lesson> Lessons { get; set; } = [];
        public virtual ICollection<User> Teachers { get; set; } = [];
        public virtual ICollection<Notification> Notifications { get; set; } = [];
        public virtual ICollection<Comment> Comments { get; set; } = [];
        //public virtual ICollection<Period> Periods { get; set; } = [];
        public virtual ICollection<TeacherLesson> LessonPlans { get; set; } = [];
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherAITools.Application.Notifications.Common
{
    public class CreateNotificationRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public bool IsRead { get; set; }
        public int UserId { get; set; }
        public int NotificationTypeId { get; set; }
    }
}
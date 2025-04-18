﻿using System.ComponentModel;

namespace TeacherAITools.Domain.Common
{
    public enum LessonStatus
    {
        [Description("Draft")]
        Draft = 1,
        [Description("Pending")]
        Pending,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected
    }
}

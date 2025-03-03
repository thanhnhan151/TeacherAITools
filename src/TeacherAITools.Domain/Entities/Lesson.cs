﻿using TeacherAITools.Domain.Entities.Base.Implementations;

namespace TeacherAITools.Domain.Entities
{
    public class Lesson : AuditableEntity
    {
        public int LessonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPublic { get; set; }

        // Foreign Key
        public int LessonTypeId { get; set; }
        public virtual LessonType LessonType { get; set; } = null!;

        public int RequirementId { get; set; }
        public virtual Requirement Requirement { get; set; } = null!;

        public int TeachingToolId { get; set; }
        public virtual TeachingTool TeachingTool { get; set; } = null!;

        public int NoteId { get; set; }
        public virtual Note Note { get; set; } = null!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int WeekId { get; set; }
        public virtual Week Week { get; set; } = null!;

        public int ModuleId { get; set; }
        public virtual Module Module { get; set; } = null!;

        //Navigation
        public virtual ICollection<Quiz> Quizzes { get; set; } = [];

        public virtual ICollection<Period> Periods { get; set; } = [];
    }
}

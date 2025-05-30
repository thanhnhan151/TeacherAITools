using TeacherAITools.Domain.Entities.Base.Implementations;

namespace TeacherAITools.Domain.Entities
{
    public class Lesson : AuditableEntity
    {
        public int LessonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public string DisapprovedReason { get; set; } = string.Empty;
        public int TotalPeriods { get; set; }
        public string SpecialAbility { get; set; } = string.Empty;
        public string GeneralCapacity { get; set; } = string.Empty;
        public string Quality { get; set; } = string.Empty;
        public string SchoolSupply { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;

        // Foreign Key
        public int LessonTypeId { get; set; }
        public virtual LessonType LessonType { get; set; } = null!;

        //public int RequirementId { get; set; }
        //public virtual Requirement Requirement { get; set; } = null!;

        //public int SchoolSupplyId { get; set; }
        //public virtual SchoolSupply SchoolSupply { get; set; } = null!;

        public int NoteId { get; set; }
        public virtual Note Note { get; set; } = null!;

        //public int UserId { get; set; }
        //public virtual User User { get; set; } = null!;

        public int WeekId { get; set; } = 1;
        public virtual Week Week { get; set; } = null!;

        public int ModuleId { get; set; }
        public virtual Module Module { get; set; } = null!;

        //public virtual Prompt Prompt { get; set; } = null!;

        public virtual StartUp StartUp { get; set; } = null!;

        public virtual KnowLedge KnowLedge { get; set; } = null!;

        public virtual Practice Practice { get; set; } = null!;

        public virtual Apply Apply { get; set; } = null!;

        //Navigation
        public virtual ICollection<Quiz> Quizzes { get; set; } = [];

        //public virtual ICollection<Period> Periods { get; set; } = [];

        public virtual ICollection<Prompt> Prompts { get; set; } = [];

        //public virtual ICollection<Blog> Blogs { get; set; } = [];
    }
}

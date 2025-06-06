﻿using TeacherAITools.Domain.Entities.Base.Implementations;

namespace TeacherAITools.Domain.Entities
{
    public class Module : AuditableEntity
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public int Semester { get; set; }
        public int TotalPeriods { get; set; } = 0;

        // Foreign Key
        public int CurriculumId { get; set; }
        public virtual Curriculum Curriculum { get; set; } = null!;

        public int GradeId { get; set; }
        public virtual Grade Grade { get; set; } = null!;

        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;

        // Navigation
        public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}

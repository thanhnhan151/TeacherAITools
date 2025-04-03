namespace TeacherAITools.Domain.Entities
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int GradeNumber { get; set; }

        public virtual ICollection<Module> Modules { get; set; } = [];
        public virtual ICollection<User> Users { get; set; } = [];
    }
}

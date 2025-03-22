using TeacherAITools.Domain.Common;

namespace TeacherAITools.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; } = string.Empty;
        public BookNumber BookNumber { get; set; }
        public bool Status { get; set; }

        // Navigation
        public virtual ICollection<Module> Modules { get; set; } = [];
    }
}

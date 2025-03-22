namespace TeacherAITools.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<Blog> Blogs { get; set; } = [];
    }
}

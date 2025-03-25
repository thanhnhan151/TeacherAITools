namespace TeacherAITools.Application.Blogs.Common
{
    public class GetBlogResponse
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}

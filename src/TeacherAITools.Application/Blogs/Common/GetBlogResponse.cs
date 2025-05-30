namespace TeacherAITools.Application.Blogs.Common
{
    public class GetBlogResponse
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string PublicationDate { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int LessonPlanId { get; set; }
    }
}

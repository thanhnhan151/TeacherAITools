using TeacherAITools.Application.Comments.Common;

namespace TeacherAITools.Application.Blogs.Common
{
    public class GetBlogDetailResponse
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string PublicationDate { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int TeacherLessonId { get; set; }
        public List<GetCommentResponse> Comments { get; set; } = [];
    }
}

namespace TeacherAITools.Application.Comments.Common
{
    public class GetCommentResponse
    {
        public int CommentId { get; set; }
        public string CommentBody { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
        public string User { get; set; } = string.Empty;
    }
}

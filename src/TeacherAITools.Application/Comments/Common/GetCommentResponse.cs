namespace TeacherAITools.Application.Comments.Common
{
    public class GetCommentResponse
    {
        public int CommentId { get; set; }
        public string ImgURL { get; set; } = string.Empty;
        public string CommentBody { get; set; } = string.Empty;
        public string TimeStamp { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
    }
}

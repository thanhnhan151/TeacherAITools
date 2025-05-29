namespace TeacherAITools.Application.Quizzes.Common
{
    public class GetQuizResponse
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; } = string.Empty;
        public string LessonName { get; set; } = string.Empty;
    }
}

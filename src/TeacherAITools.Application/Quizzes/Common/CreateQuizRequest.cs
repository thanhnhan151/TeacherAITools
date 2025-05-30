namespace TeacherAITools.Application.Quizzes.Common
{
    public class CreateQuizRequest
    {
        public string QuizName { get; set; } = string.Empty;
        public int LessonId { get; set; }
        public int UserId { get; set; }
        public List<CreateQuizQuestion> QuizQuestions { get; set; } = [];
    }
}

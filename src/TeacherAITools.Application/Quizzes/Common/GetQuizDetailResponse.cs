namespace TeacherAITools.Application.Quizzes.Common
{
    public class GetQuizDetailResponse
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; } = string.Empty;
        public List<GetQuizQuestion> QuizQuestions { get; set; } = [];
    }
}

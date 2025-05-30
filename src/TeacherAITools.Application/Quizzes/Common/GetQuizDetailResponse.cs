namespace TeacherAITools.Application.Quizzes.Common
{
    public class GetQuizDetailResponse
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Grade { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string LessonName { get; set; } = string.Empty;
        public List<GetQuizQuestion> QuizQuestions { get; set; } = [];
    }
}

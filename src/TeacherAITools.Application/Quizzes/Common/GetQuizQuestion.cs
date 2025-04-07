namespace TeacherAITools.Application.Quizzes.Common
{
    public class GetQuizQuestion
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; } = string.Empty;
        public List<GetQuizAnswer> QuizAnswers { get; set; } = [];
    }
}

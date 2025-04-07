namespace TeacherAITools.Application.Quizzes.Common
{
    public class CreateQuizQuestion
    {
        public string QuestionName { get; set; } = string.Empty;
        public List<CreateQuizAnswer> QuizAnswers { get; set; } = [];
    }
}

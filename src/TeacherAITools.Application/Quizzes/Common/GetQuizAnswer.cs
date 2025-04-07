namespace TeacherAITools.Application.Quizzes.Common
{
    public class GetQuizAnswer
    {
        public int AnswerId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}

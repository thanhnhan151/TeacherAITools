namespace TeacherAITools.Domain.Entities
{
    public class QuizAnswer
    {
        public int AnswerId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;

        // Foreign Key
        public int? QuestionId { get; set; }
        public virtual QuizQuestion QuizQuestion { get; set; } = null!;
    }
}

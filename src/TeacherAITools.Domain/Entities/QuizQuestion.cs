namespace TeacherAITools.Domain.Entities
{
    public class QuizQuestion
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; } = string.Empty;

        // Foreign Key
        public int? QuizId { get; set; }
        public virtual Quiz Quiz { get; set; } = null!;

        // Navigation
        public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = [];
    }
}

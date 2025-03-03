namespace TeacherAITools.Domain.Entities
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; } = string.Empty;

        // Foreign Key
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;

        // Navigation
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = [];
    }
}

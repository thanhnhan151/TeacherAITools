namespace TeacherAITools.Application.Prompts.Commnon
{
    public class GetPromptResponse
    {
        public int PromptId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string LessonName { get; set; } = string.Empty;
    }
}

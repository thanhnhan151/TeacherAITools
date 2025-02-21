using TeacherAITools.Application.Common.Interfaces.Services;

namespace TeacherAITools.Infrastructure.Services
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

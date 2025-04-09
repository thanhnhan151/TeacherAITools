using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface IPeriodRepository : IRepository<Period>
    {
        int GetLastIdPeriod();
        Task<bool> IsCompleteForTeacherAsync(int lessonId, int userId);
    }
}
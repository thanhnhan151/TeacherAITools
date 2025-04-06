using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface ILessonsRepository : IRepository<Lesson>
    {
        int GetLastIdLesson();
    }
}
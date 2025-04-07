using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class LessonsRepository(
       TeacherAIToolsDbContext dbContext,
       ILogger logger) : Repository<Lesson>(dbContext, logger), ILessonsRepository
    {
        public int GetLastIdLesson(){
            var lesson = _dbContext.Lessons.OrderBy(e => e.LessonId).LastOrDefault();
            return lesson is not null ? lesson.LessonId : 0;
        }
    }
}
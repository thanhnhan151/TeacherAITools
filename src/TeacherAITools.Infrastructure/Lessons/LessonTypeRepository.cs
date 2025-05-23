using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class LessonTypeRepository(
       TeacherAIToolsDbContext dbContext,
       ILogger logger) : Repository<LessonType>(dbContext, logger), ILessonTypeRepository
    {
    }
}
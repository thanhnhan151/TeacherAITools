using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class LessonsRepository:  Repository<Lesson>, ILessonsRepository
    {
         public LessonsRepository(
            TeacherAIToolsDbContext dbContext
            , ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
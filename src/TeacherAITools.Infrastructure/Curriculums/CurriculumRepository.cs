using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumRepository: Repository<Curriculum>, ICurriculumRepository
    {
        public CurriculumRepository(
            TeacherAIToolsDbContext dbContext
            , ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Prompts
{
    public class PromptRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<Prompt>(dbContext, logger), IPromptRepository
    {
    }
}

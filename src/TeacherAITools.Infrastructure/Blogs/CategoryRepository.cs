using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Blogs
{
    public class CategoryRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<Category>(dbContext, logger), ICategoryRepository
    {
    }
}

using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Modules
{
    public class BookRepository(
       TeacherAIToolsDbContext dbContext,
       ILogger logger) : Repository<Book>(dbContext, logger), IBookRepository
    {
    }
}
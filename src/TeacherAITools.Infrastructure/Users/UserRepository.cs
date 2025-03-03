using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(
            TeacherAIToolsDbContext dbContext
            , ILogger logger) : base(dbContext, logger)
        {
        }
    }
}

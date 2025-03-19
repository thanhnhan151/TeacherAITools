﻿using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Users
{
    public class UserRepository(
        TeacherAIToolsDbContext dbContext,
        ILogger logger) : Repository<User>(dbContext, logger), IUserRepository
    {
    }
}

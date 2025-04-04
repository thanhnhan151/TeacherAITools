using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class SchoolSupplyRepository(
       TeacherAIToolsDbContext dbContext,
       ILogger logger) : Repository<SchoolSupply>(dbContext, logger), ISchoolSupplyRepository
    {
    }
}
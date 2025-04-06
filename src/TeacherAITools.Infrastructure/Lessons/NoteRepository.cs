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
    public class NoteRepository(
       TeacherAIToolsDbContext dbContext,
       ILogger logger) : Repository<Note>(dbContext, logger), INoteRepository
    {
    }
}
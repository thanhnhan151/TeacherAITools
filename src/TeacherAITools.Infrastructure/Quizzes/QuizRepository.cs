using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Quizzes
{
    public class QuizRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<Quiz>(dbContext, logger), IQuizRepository
    {
    }
}

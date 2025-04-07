using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Quizzes
{
    public class QuizAnswerRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<QuizAnswer>(dbContext, logger), IQuizAnswerRepository
    {
        public int GetLastIdAnswer()
        {
            var answer = _dbContext.QuizAnswers.OrderBy(e => e.AnswerId).LastOrDefault();
            return answer is not null ? answer.AnswerId : 0;
        }
    }
}

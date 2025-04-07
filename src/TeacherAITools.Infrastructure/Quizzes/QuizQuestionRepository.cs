using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Quizzes
{
    public class QuizQuestionRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<QuizQuestion>(dbContext, logger), IQuizQuestionRepository
    {
        public int GetLastIdQuestion()
        {
            var question = _dbContext.QuizQuestions.OrderBy(e => e.QuestionId).LastOrDefault();
            return question is not null ? question.QuestionId : 0;
        }
    }
}

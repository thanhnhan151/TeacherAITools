using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Quizzes
{
    public class QuizRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<Quiz>(dbContext, logger), IQuizRepository
    {
        public async Task<PaginatedList<Quiz>> PaginatedListAsync(string? searchTerm, string? sortColumn, string? sortOrder, int? lessonId, int page, int pageSize)
        {
            IQueryable<Quiz> quizzesQuery = _dbContext.Quizzes
                .Include(u => u.Lesson);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                quizzesQuery = quizzesQuery.Where(c =>
                    c.QuizName.Contains(searchTerm));
            }

            if (lessonId != null)
            {
                quizzesQuery = quizzesQuery.Where(c => c.LessonId == lessonId);
            }

            if (sortOrder?.ToLower() == "asc")
            {
                quizzesQuery = quizzesQuery.OrderBy(GetSortProperty(sortColumn));
            }
            else
            {
                quizzesQuery = quizzesQuery.OrderByDescending(GetSortProperty(sortColumn));
            }

            var quizzes = await PaginatedList<Quiz>.CreateAsync(quizzesQuery, page, pageSize);

            return quizzes;
        }

        private static Expression<Func<Quiz, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => quiz => quiz.QuizName,
            //"dob" => user => user.DoB,
            _ => quiz => quiz.QuizId
        };

        public int GetLastIdQuiz()
        {
            var quiz = _dbContext.Quizzes.OrderBy(e => e.QuizId).LastOrDefault();
            return quiz is not null ? quiz.QuizId : 0;
        }
    }
}

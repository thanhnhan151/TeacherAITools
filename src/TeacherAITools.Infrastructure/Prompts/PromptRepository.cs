using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Prompts
{
    public class PromptRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<Prompt>(dbContext, logger), IPromptRepository
    {
        public async Task<PaginatedList<Prompt>> PaginatedListAsync(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize)
        {
            IQueryable<Prompt> promptsQuery = _dbContext.Prompts
                .Include(p => p.Lesson);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                promptsQuery = promptsQuery.Where(c =>
                    c.Description.Contains(searchTerm));
            }

            if (sortOrder?.ToLower() == "asc")
            {
                promptsQuery = promptsQuery.OrderBy(GetSortProperty(sortColumn));
            }
            else
            {
                promptsQuery = promptsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }

            var prompts = await PaginatedList<Prompt>.CreateAsync(promptsQuery, page, pageSize);

            return prompts;
        }

        private static Expression<Func<Prompt, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => prompt => prompt.Description,
            //"dob" => user => user.DoB,
            _ => prompt => prompt.PromptId
        };

        public async Task<bool> IsLessonIdPresentAsync(int lessonId) => await _dbContext.Prompts.AnyAsync(p => p.LessonId == lessonId);

        public int GetLastIdPrompt()
        {
            var answer = _dbContext.Prompts.OrderBy(e => e.PromptId).LastOrDefault();
            return answer is not null ? answer.PromptId : 0;
        }

        public async Task<bool> IsBelongedToTeacherAsync(int userId, int lessonId)
        => await _dbContext.Prompts.AnyAsync(t => t.UserId == userId && t.LessonId == lessonId);
    }
}

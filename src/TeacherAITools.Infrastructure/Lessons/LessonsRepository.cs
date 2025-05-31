using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class LessonsRepository(
       TeacherAIToolsDbContext dbContext,
       ILogger logger) : Repository<Lesson>(dbContext, logger), ILessonsRepository
    {
        public int GetLastIdLesson()
        {
            var lesson = _dbContext.Lessons.OrderBy(e => e.LessonId).LastOrDefault();
            return lesson is not null ? lesson.LessonId : 0;
        }

        public async Task<PaginatedList<Lesson>> PaginatedListAsync(string? searchTerm, string? sortColumn, string? sortOrder, int? lessonTypeId, int? moduleId, int? isActive, int page, int pageSize)
        {
            IQueryable<Lesson> blogsQuery = _dbContext.Lessons
                .Include(u => u.LessonType)
                .Include(u => u.Module)
                .Include(u => u.Note);

            switch (isActive)
            {
                case 1:
                    blogsQuery = blogsQuery.Where(u => u.IsActive);
                    break;
                case 0:
                    blogsQuery = blogsQuery.Where(u => u.IsActive == false);
                    break;
                default: break;
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                blogsQuery = blogsQuery.Where(c =>
                    c.Name.Contains(searchTerm));
            }

            if (lessonTypeId != null)
            {
                blogsQuery = blogsQuery.Where(c => c.LessonTypeId == lessonTypeId);
            }

            if (moduleId != null)
            {
                blogsQuery = blogsQuery.Where(c => c.ModuleId == moduleId);
            }

            if (sortOrder?.ToLower() == "asc")
            {
                blogsQuery = blogsQuery.OrderBy(GetSortProperty(sortColumn));
            }
            else
            {
                blogsQuery = blogsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }

            var blogs = await PaginatedList<Lesson>.CreateAsync(blogsQuery, page, pageSize);

            return blogs;
        }

        private static Expression<Func<Lesson, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => blog => blog.Name,
            //"dob" => user => user.DoB,
            _ => blog => blog.LessonId
        };
    }
}
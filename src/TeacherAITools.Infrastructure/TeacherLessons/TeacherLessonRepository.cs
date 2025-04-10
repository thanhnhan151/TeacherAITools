using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.TeacherLessons
{
    public class TeacherLessonRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<TeacherLesson>(dbContext, logger), ITeacherLessonRepository
    {
        public async Task<PaginatedList<TeacherLesson>> PaginatedListAsync(string? searchTerm, string? sortColumn, string? sortOrder, int? moduleId, int? lessonId, int? userId, int? gradeId, LessonStatus status, int page, int pageSize)
        {
            IQueryable<TeacherLesson> teacherLessonsQuery = _dbContext.TeacherLessons
                .Include(u => u.Prompt)
                        .ThenInclude(w => w.Lesson)
                                    .ThenInclude(w => w.Module)
                .Include(u => u.User);

            switch (status)
            {
                case LessonStatus.Draft:
                    teacherLessonsQuery = teacherLessonsQuery.Where(u => u.Status == LessonStatus.Draft);
                    break;
                case LessonStatus.Pending:
                    teacherLessonsQuery = teacherLessonsQuery.Where(u => u.Status == LessonStatus.Pending);
                    break;
                case LessonStatus.Approved:
                    teacherLessonsQuery = teacherLessonsQuery.Where(u => u.Status == LessonStatus.Approved);
                    break;
                case LessonStatus.Rejected:
                    teacherLessonsQuery = teacherLessonsQuery.Where(u => u.Status == LessonStatus.Rejected);
                    break;
                //case LessonStatus.Cancelled:
                //    teacherLessonsQuery = teacherLessonsQuery.Where(u => u.Status == LessonStatus.Cancelled);
                //    break;
                default: break;
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                teacherLessonsQuery = teacherLessonsQuery.Where(c =>
                    c.Prompt.Lesson.Name.Contains(searchTerm));
            }

            if (moduleId != null)
            {
                teacherLessonsQuery = teacherLessonsQuery.Where(c => c.Prompt.Lesson.ModuleId == moduleId);
            }

            if (lessonId != null)
            {
                teacherLessonsQuery = teacherLessonsQuery.Where(c => c.Prompt.LessonId == lessonId);
            }

            if (userId != null)
            {
                teacherLessonsQuery = teacherLessonsQuery.Where(c => c.UserId == userId);
            }

            if (gradeId != null)
            {
                teacherLessonsQuery = teacherLessonsQuery.Where(c => c.Prompt.Lesson.Module.GradeId == gradeId);
            }

            if (sortOrder?.ToLower() == "asc")
            {
                teacherLessonsQuery = teacherLessonsQuery.OrderBy(GetSortProperty(sortColumn));
            }
            else
            {
                teacherLessonsQuery = teacherLessonsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }

            var teacherLessons = await PaginatedList<TeacherLesson>.CreateAsync(teacherLessonsQuery, page, pageSize);

            return teacherLessons;
        }

        private static Expression<Func<TeacherLesson, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => lesson => lesson.Prompt.Lesson.Name,
            //"dob" => user => user.DoB,
            _ => lesson => lesson.TeacherLessonId
        };

        public async Task<bool> IsBelongedToTeacherAsync(int userId, int promptId) => await _dbContext.TeacherLessons.AnyAsync(t => t.UserId == userId && t.PromptId == promptId);
    }
}

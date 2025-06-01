using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Blogs
{
    public class BlogRepository(TeacherAIToolsDbContext dbContext, ILogger logger) : Repository<Blog>(dbContext, logger), IBlogRepository
    {
        public async Task<PaginatedList<Blog>> PaginatedListAsync(string? searchTerm, string? sortColumn, string? sortOrder, int? userId, int? categoryId, int? isActive, int page, int pageSize)
        {
            IQueryable<Blog> blogsQuery = _dbContext.Blogs
                .Include(u => u.User)
                .Include(u => u.Category)
                .Include(u => u.LessonPlan);

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
                    c.Title.Contains(searchTerm));
            }

            if (categoryId != null)
            {
                blogsQuery = blogsQuery.Where(c => c.CategoryId == categoryId);
            }

            if (userId != null)
            {
                blogsQuery = blogsQuery.Where(c => c.UserId == userId);
            }

            if (sortOrder?.ToLower() == "asc")
            {
                blogsQuery = blogsQuery.OrderBy(GetSortProperty(sortColumn));
            }
            else
            {
                blogsQuery = blogsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }

            var blogs = await PaginatedList<Blog>.CreateAsync(blogsQuery, page, pageSize);

            return blogs;
        }

        private static Expression<Func<Blog, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "date" => blog => blog.PublicationDate,
            //"dob" => user => user.DoB,
            _ => blog => blog.BlogId
        };
    }
}

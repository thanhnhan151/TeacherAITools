using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Schools
{
    public class SchoolRepository(
        TeacherAIToolsDbContext dbContext,
        ILogger logger) : Repository<School>(dbContext, logger), ISchoolRepository
    {
        public async Task<PaginatedList<School>> PaginatedListAsync(string? searchTerm, string? sortColumn, string? sortOrder, bool isActive, int page, int pageSize)
        {
            IQueryable<School> schoolsQuery = _dbContext.Schools
                .Include(u => u.Ward)
                        .ThenInclude(w => w.District)
                                    .ThenInclude(d => d.City);

            if (isActive) schoolsQuery = schoolsQuery.Where(u => u.Status);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                schoolsQuery = schoolsQuery.Where(c =>
                    c.Name.Contains(searchTerm));
            }

            if (sortOrder?.ToLower() == "asc")
            {
                schoolsQuery = schoolsQuery.OrderBy(GetSortProperty(sortColumn));
            }
            else
            {
                schoolsQuery = schoolsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }

            var schools = await PaginatedList<School>.CreateAsync(schoolsQuery, page, pageSize);

            return schools;
        }

        private static Expression<Func<School, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => School => School.Name,
            //"dob" => school => school.DoB,
            _ => School => School.SchoolId
        };
    }
}

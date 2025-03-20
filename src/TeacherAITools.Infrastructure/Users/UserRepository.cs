using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;
using TeacherAITools.Infrastructure.Common.Persistence;

namespace TeacherAITools.Infrastructure.Users
{
    public class UserRepository(
        TeacherAIToolsDbContext dbContext,
        ILogger logger) : Repository<User>(dbContext, logger), IUserRepository
    {
        public async Task<PaginatedList<User>> PaginatedListAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int? roleId,
            bool isActive,
            int page,
            int pageSize)
        {
            IQueryable<User> usersQuery = _dbContext.Users
                .Include(u => u.Role)
                .Include(u => u.Manager)
                .Include(u => u.School);

            if (isActive) usersQuery = usersQuery.Where(u => u.IsActive);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                usersQuery = usersQuery.Where(c =>
                    c.Fullname.Contains(searchTerm));
            }

            if (roleId != null)
            {
                usersQuery = usersQuery.Where(c => c.RoleId == roleId);
            }

            if (sortOrder?.ToLower() == "asc")
            {
                usersQuery = usersQuery.OrderBy(GetSortProperty(sortColumn));
            }
            else
            {
                usersQuery = usersQuery.OrderByDescending(GetSortProperty(sortColumn));
            }

            var users = await PaginatedList<User>.CreateAsync(usersQuery, page, pageSize);

            return users;
        }

        private static Expression<Func<User, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => user => user.Fullname,
            //"dob" => user => user.DoB,
            _ => user => user.UserId
        };
    }
}

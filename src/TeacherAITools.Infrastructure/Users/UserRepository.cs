using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Domain.Common;
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
            int? gradeId,
            bool isActive,
            int page,
            int pageSize)
        {
            IQueryable<User> usersQuery = _dbContext.Users
                .Where(u => u.RoleId != 1)
                .Include(u => u.Role)
                .Include(u => u.Manager)
                .Include(u => u.School)
                .Include(u => u.Grade)
                .Include(u => u.Ward)
                        .ThenInclude(w => w.District)
                                    .ThenInclude(d => d.City);

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

            if (gradeId != null)
            {
                usersQuery = usersQuery.Where(c => c.GradeId == gradeId);
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

        public async Task<bool> CheckSchoolManagerAsync(int roleId, int schoolId)
        {
            var result = await _dbContext.Users.Where(u => u.RoleId == (int)AvailableRole.SubjectSpecialistManager).FirstOrDefaultAsync();

            return true;
        }
    }
}

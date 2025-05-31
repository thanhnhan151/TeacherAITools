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
            int? schoolId,
            int? isActive,
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

            switch (isActive)
            {
                case 1:
                    usersQuery = usersQuery.Where(u => u.IsActive);
                    break;
                case 0:
                    usersQuery = usersQuery.Where(u => u.IsActive == false);
                    break;
                default: break;
            }

            //if (isActive == false)
            //{
            //    usersQuery = usersQuery.Where(u => u.IsActive == false);
            //}

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

        public async Task<int> CheckGradeManagerAsync(int roleId, int gradeId)
        {
            var result = await _dbContext.Users.Where(u => u.GradeId == gradeId
                                                                && u.RoleId == roleId)
                                                       .FirstOrDefaultAsync();

            if (result is not null) return 1;

            return 0;
        }

        public async Task<int> GetSchoolManagerAsync(int gradeId)
        {
            var result = await _dbContext.Users.Where(u => u.RoleId == (int)AvailableRole.SubjectSpecialistManager && u.GradeId == gradeId).FirstOrDefaultAsync();

            if (result is not null) return result.UserId;

            return 0;
        }

        public async Task<User?> SendOtpAsync(string email)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            return result is not null ? result : null;
        }

        public async Task<User?> ResetPasswordAsync(string Otp)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.ResetPasswordOtp == Otp);
            return result is not null ? result : null;
        }
    }
}

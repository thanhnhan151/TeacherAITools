﻿using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PaginatedList<User>> PaginatedListAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int? roleId
            , int? gradeId
            , int? schooId
            , int? isActive
            , int page
            , int pageSize);

        Task<int> CheckSchoolManagerAsync(int roleId, int gradeId, int schoolId);

        Task<int> GetSchoolManagerAsync(int gradeId, int schoolId);

        Task<User?> SendOtpAsync(string email);

        Task<User?> ResetPasswordAsync(string Otp);
    }
}

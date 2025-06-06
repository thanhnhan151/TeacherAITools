﻿using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface IPromptRepository : IRepository<Prompt>
    {
        Task<PaginatedList<Prompt>> PaginatedListAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<bool> IsLessonIdPresentAsync(int lessonId);

        int GetLastIdPrompt();

        Task<bool> IsBelongedToTeacherAsync(int userId, int lessonId);
    }
}

using AutoMapper;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Application.Books.Common;
using TeacherAITools.Application.Categories.Common;
using TeacherAITools.Application.Cities.Common;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Application.Districts.Common;
using TeacherAITools.Application.Grades.Common;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Application.LessonTypes.Common;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Application.Notes.Common;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Application.Quizzes.Common;
using TeacherAITools.Application.Requirements.Common;
using TeacherAITools.Application.Roles.Common;
using TeacherAITools.Application.Schools.Common;
using TeacherAITools.Application.SchoolSupplies.Common;
using TeacherAITools.Application.SchoolYears.Common;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Application.Weeks.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Common.Mappings
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region City
            CreateMap<City, GetCityResponse>();
            CreateMap<City, GetCityDetailResponse>();
            #endregion

            #region District
            CreateMap<District, GetDistrictResponse>();
            CreateMap<District, GetDistrictDetailResponse>();
            #endregion

            #region Ward
            CreateMap<Ward, GetWardResponse>();
            #endregion

            #region User
            CreateMap<User, GetUserResponse>()
                .ForMember(u => u.School, u => u.MapFrom(u => u.School.Name))
                .ForMember(u => u.Manager, u => u.MapFrom(u => u.ManagerId != null ? u.Manager.Fullname : "N/A"))
                .ForMember(u => u.Role, u => u.MapFrom(u => u.Role.RoleName))
                .ForMember(u => u.Grade, u => u.MapFrom(u => $"Lớp {u.Grade.GradeNumber}"))
                .ForMember(u => u.Gender, u => u.MapFrom(u => u.Gender.GetDescription()))
                .ForMember(m => m.DateOfBirth, m => m.MapFrom(m => m.DateOfBirth.GetFormatDate()))
                .ForMember(m => m.Address, m => m.MapFrom(m => $"{m.Address}, phường {m.Ward.WardName}, quận {m.Ward.District.DistrictName}, TP. {m.Ward.District.City.CityName}"));
            CreateMap<User, GetUserUpdateResponse>()
                .ForMember(u => u.School, u => u.MapFrom(u => u.School.Name))
                .ForMember(u => u.Manager, u => u.MapFrom(u => u.ManagerId != null ? u.Manager.Fullname : "N/A"))
                .ForMember(u => u.Role, u => u.MapFrom(u => u.Role.RoleName))
                .ForMember(u => u.Grade, u => u.MapFrom(u => $"Lớp {u.Grade.GradeNumber}"))
                .ForMember(u => u.Gender, u => u.MapFrom(u => u.Gender.GetDescription()))
                .ForMember(m => m.DateOfBirth, m => m.MapFrom(m => m.DateOfBirth.GetFormatDate()));
            CreateMap<PaginatedList<User>, PaginatedList<GetUserResponse>>();
            #endregion

            #region Category
            CreateMap<Category, GetCategoryResponse>();
            #endregion

            #region Blog
            CreateMap<Blog, GetBlogResponse>()
                .ForMember(b => b.Category, b => b.MapFrom(b => b.Category.CategoryName))
                .ForMember(m => m.Name, m => m.MapFrom(m => $"{m.User.Fullname} - Giáo viên lớp {m.User.Grade}"))
                .ForMember(m => m.PublicationDate, m => m.MapFrom(m => m.PublicationDate.GetFormatDateTime()));
            CreateMap<Blog, GetBlogDetailResponse>()
                .ForMember(b => b.Category, b => b.MapFrom(b => b.Category.CategoryName))
                .ForMember(m => m.Name, m => m.MapFrom(m => $"{m.User.Fullname} - Giáo viên lớp {m.User.Grade}"))
                .ForMember(m => m.PublicationDate, m => m.MapFrom(m => m.PublicationDate.GetFormatDateTime()));
            CreateMap<PaginatedList<Blog>, PaginatedList<GetBlogResponse>>();
            #endregion

            #region Comment
            CreateMap<Comment, GetCommentResponse>()
                .ForMember(m => m.ImgURL, m => m.MapFrom(m => m.User.ImgURL))
                .ForMember(m => m.User, m => m.MapFrom(m => $"{m.User.Fullname} - Giáo viên lớp {m.User.Grade}"))
                .ForMember(m => m.TimeStamp, m => m.MapFrom(m => m.TimeStamp.GetFormatDateTime()));
            #endregion

            #region School
            CreateMap<School, GetSchoolResponse>()
                .ForMember(s => s.Ward, s => s.MapFrom(s => s.Ward.WardName))
                .ForMember(s => s.District, s => s.MapFrom(s => s.Ward.District.DistrictName))
                .ForMember(s => s.City, s => s.MapFrom(s => s.Ward.District.City.CityName));
            CreateMap<PaginatedList<School>, PaginatedList<GetSchoolResponse>>();
            #endregion

            #region Grade
            CreateMap<Grade, GetGradeResponse>();
            CreateMap<Grade, GetGradeDetailResponse>();
            CreateMap<Module, GetModuleItem>();
            #endregion

            #region Role
            CreateMap<Role, GetRoleResponse>();
            #endregion

            #region Module
            CreateMap<Module, GetModuleDetailResponse>();
            CreateMap<Lesson, GetLessonItem>()
                .ForMember(l => l.LessonType, l => l.MapFrom(l => l.LessonType.LessonTypeName));
            #endregion

            #region Quiz
            CreateMap<Quiz, GetQuizResponse>()
                .ForMember(q => q.Name, q => q.MapFrom(q => $"{q.User.Fullname} - Giáo viên lớp {q.User.GradeId}"))
                .ForMember(q => q.Grade, q => q.MapFrom(q => q.Lesson.Module.GradeId))
                .ForMember(q => q.ModuleName, q => q.MapFrom(q => q.Lesson.Module.Name))
                .ForMember(q => q.LessonName, q => q.MapFrom(q => q.Lesson.Name));
            CreateMap<Quiz, GetQuizDetailResponse>()
                .ForMember(q => q.Name, q => q.MapFrom(q => $"{q.User.Fullname} - Giáo viên lớp {q.User.GradeId}"))
                .ForMember(q => q.Grade, q => q.MapFrom(q => q.Lesson.Module.GradeId))
                .ForMember(q => q.ModuleName, q => q.MapFrom(q => q.Lesson.Module.Name))
                .ForMember(q => q.LessonName, q => q.MapFrom(q => q.Lesson.Name));
            CreateMap<QuizQuestion, GetQuizQuestion>();
            CreateMap<QuizAnswer, GetQuizAnswer>();
            CreateMap<PaginatedList<Quiz>, PaginatedList<GetQuizResponse>>();
            #endregion

            #region Lesson
            CreateMap<LessonType, GetLessonTypeResponse>();
            CreateMap<Note, GetNoteResponse>();
            CreateMap<Requirement, GetRequirementResponse>();
            CreateMap<Week, GetWeekResponse>();
            CreateMap<SchoolSupply, GetSchoolSupplyResponse>();
            CreateMap<SchoolYear, GetSchoolYearResponse>();
            #endregion

            #region Prompt
            CreateMap<Prompt, GetPromptResponse>();
            CreateMap<PaginatedList<Prompt>, PaginatedList<GetPromptResponse>>();
            #endregion

            #region TeacherLesson
            CreateMap<TeacherLesson, GetTeacherLessonResponse>()
                .ForMember(c => c.Status, c => c.MapFrom(c => c.Status.GetDescription()))
                .ForMember(c => c.Fullname, c => c.MapFrom(c => c.User.Fullname))
                .ForMember(c => c.CreatedAt, c => c.MapFrom(c => c.CreatedAt.GetFormatDateTime()))
                .ForMember(c => c.Lesson, c => c.MapFrom(c => c.Prompt.Lesson.Name))
                .ForMember(c => c.Module, c => c.MapFrom(c => c.Prompt.Lesson.Module.Name))
                .ForMember(c => c.Grade, c => c.MapFrom(c => c.Prompt.Lesson.Module.GradeId));
            CreateMap<TeacherLesson, GetDetailTeacherLessonResponse>()
                .ForMember(c => c.Status, c => c.MapFrom(c => c.Status.GetDescription()))
                .ForMember(c => c.Fullname, c => c.MapFrom(c => c.User.Fullname))
                .ForMember(c => c.Lesson, c => c.MapFrom(c => c.Prompt.Lesson.Name))
                .ForMember(c => c.CreatedAt, c => c.MapFrom(c => c.CreatedAt.GetFormatDateTime()))
                .ForMember(c => c.TotalPeriods, c => c.MapFrom(c => c.Prompt.Lesson.TotalPeriods))
                .ForMember(c => c.Module, c => c.MapFrom(c => c.Prompt.Lesson.Module.Name))
                .ForMember(c => c.Grade, c => c.MapFrom(c => c.Prompt.Lesson.Module.GradeId));
            CreateMap<TeacherLesson, GetUserLessonsResponse>()
                .ForMember(p => p.Lesson, p => p.MapFrom(p => p.Prompt.Lesson.Name));
            CreateMap<PaginatedList<TeacherLesson>, PaginatedList<GetTeacherLessonResponse>>();
            #endregion

            #region Curriculum
            CreateMap<Curriculum, GetDetailCurriculumResponse>()
                .ForMember(c => c.Year, c => c.MapFrom(c => c.SchoolYear.Year))
                .ForMember(c => c.SchoolYearId, c => c.MapFrom(c => c.SchoolYear.SchoolYearId));
            CreateMap<CurriculumDetail, DetailItem>()
                .ForMember(c => c.CurriculumTopic, c => c.MapFrom(c => c.CurriculumSubSection.CurriculumSection.CurriculumTopic.CurriculumTopicName))
                .ForMember(c => c.CurriculumSection, c => c.MapFrom(c => c.CurriculumSubSection.CurriculumSection.CurriculumSectionName))
                .ForMember(c => c.CurriculumSubSection, c => c.MapFrom(c => c.CurriculumSubSection.CurriculumSubSectionName));
            CreateMap<CurriculumActivity, ActivityItem>();
            CreateMap<CurriculumFeedback, GetCurriculumFeedbackResponse>()
                .ForMember(m => m.ImgURL, m => m.MapFrom(m => m.User.ImgURL))
                .ForMember(c => c.TimeStamp, c => c.MapFrom(c => c.TimeStamp.GetFormatDateTime()))
                .ForMember(c => c.User, c => c.MapFrom(c => $"{c.User.Username} - Giáo viên lớp {c.User.Grade}"));
            CreateMap<CurriculumSubSection, GetCurriculumSubSectionsResponse>();
            #endregion

            #region Lesson
            CreateMap<GetLessonDetailResponse, Lesson>();
            #endregion

            #region
            CreateMap<Book, GetBookResponse>();
            #endregion
        }
    }
}

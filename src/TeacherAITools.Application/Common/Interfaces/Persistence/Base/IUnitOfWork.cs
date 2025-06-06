﻿namespace TeacherAITools.Application.Common.Interfaces.Persistence.Base
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IModuleRepository Modules { get; }

        ICurriculumRepository Curriculums { get; }

        ISchoolRepository Schools { get; }

        ICityRepository Cities { get; }

        IDistrictRepository Districts { get; }

        ILessonsRepository Lessons { get; }

        ICategoryRepository Categories { get; }

        IBlogRepository Blogs { get; }

        ICommentRepository Comments { get; }

        IRoleRepository Roles { get; }

        INotificationRepository Notifications { get; }

        IGradeRepository Grades { get; }

        ILessonTypeRepository LessonTypes { get; }

        IRequirementRepository Requirements { get; }

        INoteRepository Notes { get; }

        ISchoolSupplyRepository SchoolSupplies { get; }

        IWeekRepository Weeks { get; }

        ISchoolYearRepository SchoolYears { get; }

        IBookRepository Books { get; }

        IPeriodRepository Periods { get; }

        IPeriodDetailRepository PeriodDetails { get; }

        IPromptRepository Prompts { get; }

        IQuizRepository Quizzes { get; }

        IQuizQuestionRepository QuizQuestions { get; }

        IQuizAnswerRepository QuizAnswers { get; }

        ITeacherLessonRepository TeacherLessons { get; }

        ILessonHistoryRepository LessonHistories { get; }

        ICurriculumFeedbackRepository CurriculumFeedbacks { get; }

        ICurriculumDetailRepository CurriculumDetails { get; }

        ICurriculumSectionRepository CurriculumSections { get; }

        ICurriculumSubSectionRepository CurriculumSubSections { get; }

        ICurriculumTopicRepository CurriculumTopics { get; }

        IStartUpRepository StartUps { get; }

        IKnowLedgeRepository KnowLedeges { get; }

        IPracticeRepository Practices { get; }

        IApplyRepository Applies { get; }

        Task CompleteAsync();

        void Complete();
    }
}

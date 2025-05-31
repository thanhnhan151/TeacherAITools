using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using System.Text;
using TeacherAITools.Application.Blogs.Commands.CreateBlog;
using TeacherAITools.Application.Blogs.Commands.DisableBlog;
using TeacherAITools.Application.Blogs.Commands.UpdateBlog;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Application.Blogs.Queries.GetBlogById;
using TeacherAITools.Application.Blogs.Queries.GetBlogs;
using TeacherAITools.Application.Categories.Common;
using TeacherAITools.Application.Categories.Queries.GetCategories;
using TeacherAITools.Application.Cities.Common;
using TeacherAITools.Application.Cities.Queries.GetCities;
using TeacherAITools.Application.Cities.Queries.GetDistrictsByCityId;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.Districts.Common;
using TeacherAITools.Application.Lessons.Commands.CreateLesson;
using TeacherAITools.Application.Lessons.Commands.DeleteLesson;
using TeacherAITools.Application.Lessons.Commands.UpdateIsApprovedLesson;
using TeacherAITools.Application.Lessons.Commands.UpdateLesson;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Application.Lessons.Queries.GetLessonById;
using TeacherAITools.Application.Lessons.Queries.GetLessons;
using TeacherAITools.Application.Lessons.Queries.GetPromptById;
using TeacherAITools.Application.Modules.Commands.CreateModule;
using TeacherAITools.Application.Modules.Commands.DeleteModule;
using TeacherAITools.Application.Modules.Commands.UpdateModule;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Application.Modules.Queries.GetLessonsByModuleId;
using TeacherAITools.Application.Modules.Queries.GetModuleById;
using TeacherAITools.Application.Modules.Queries.GetModules;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Application.TeacherLessons.Commands.CreatePendingTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Commands.CreateTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Commands.UpdateStatusTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Commands.UpdateTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessonById;
using TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessons;
using TeacherAITools.Application.Users.Commands.ChangePassword;
using TeacherAITools.Application.Users.Commands.CheckOtp;
using TeacherAITools.Application.Users.Commands.CreateUser;
using TeacherAITools.Application.Users.Commands.DisableUser;
using TeacherAITools.Application.Users.Commands.SendEmail;
using TeacherAITools.Application.Users.Commands.UpdateUser;
using TeacherAITools.Application.Users.Commands.UploadProfileImg;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Application.Users.Queries.GetTeacherLessonsById;
using TeacherAITools.Application.Users.Queries.GetUserById;
using TeacherAITools.Application.Users.Queries.GetUsers;
using TeacherAITools.Application.Users.Queries.GetUserUpdateById;
using TeacherAITools.Application.Weeks.Common;
using TeacherAITools.Application.Weeks.Queries.GetWeeks;
using TeacherAITools.Domain.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Entities.Base.Implementations;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.UnitTests;

[TestClass]
public sealed class Test1
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMapper> _mapperMock;

    private Mock<IWeekRepository> _weeksRepositoryMock;
    private Mock<ITeacherLessonRepository> _teacherLessonsRepoMock;
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IRoleRepository> _roleRepositoryMock;
    private Mock<ISchoolRepository> _schoolRepositoryMock;
    private Mock<IGradeRepository> _gradeRepositoryMock;
    private Mock<IEmailService> _emailServiceMock;
    private Mock<IUploadFileService> _uploadFileServiceMock;
    private Mock<ICurrentUserService> _currentUserServiceMock;
    private Mock<ILessonsRepository> _lessonRepositoryMock;
    private Mock<IPromptRepository> _promptRepositoryMock;
    private Mock<ITeacherLessonRepository> _teacherLessonRepoMock;
    private Mock<IDateTimeProvider> _dateTimeProviderMock;
    private Mock<ILessonHistoryRepository> _lessonHistoryRepoMock;
    private Mock<IModuleRepository> _moduleRepoMock;
    private Mock<ICurriculumRepository> _curriculumRepoMock;
    private Mock<IBookRepository> _bookRepoMock;

    private GetWeeksQueryHandler _getWeeksQueryHandler;
    private GetTeacherLessonsByIdQueryHandler _getTeacherLessonsByIdQueryHandler;
    private GetUserByIdQueryHandler _getUserByIdQueryHandler;
    private GetUsersQueryHandler _getUsersQueryHandler;
    private GetUserUpdateByIdQueryHandler _getUserUpdateByIdQueryHandler;
    private ChangePasswordCommandHandler _changePasswordCommandHandler;
    private CheckOtpCommandHandler _checkOtpCommandHandler;
    private CreateUserCommandHandler _createUserCommandHandler;
    private DisableUserCommandHandler _disableUserCommandHandler;
    private SendEmailCommandHandler _sendEmailCommandHandler;
    private UpdateUserCommandHandler _updateUserCommandHandler;
    private GetLessonByIdQueryHandler _getLessonByIdQueryHandler;
    private GetLessonsQueryHandler _getLessonsQueryHandler;
    private GetPromptByIdQueryHandler _getPromptByIdQueryHandler;
    private CreateLessonCommandHandler _createLessonCommandHandler;
    private DeleteLessonCommandHandler _deleteLessonCommandHandler;
    private UpdateIsApprovedLessonCommandHandler _updateIsApprovedLessonCommandHandler;
    private UpdateLessonCommandHandler _updateLessonCommandHandler;
    private GetTeacherLessonByIdQueryHandler _getTeacherLessonByIdQueryHandler;
    private GetTeacherLessonsQueryHandler _getTeacherLessonsQueryHandler;
    private CreatePendingTeacherLessonCommandHandler _createPendingTeacherLessonCommandHandler;
    private CreateTeacherLessonCommandHandler _createTeacherLessonCommandHandler;
    private UpdateStatusTeacherLessonCommandHandler _updateStatusTeacherLessonCommandHandler;
    private UpdateTeacherLessonCommandHandler _updateTeacherLessonCommandHandler;
    private CreateModuleCommandHandler _createModuleCommandHandler;
    private DeleteModuleCommandHandler _deleteModuleCommandHandler;
    private UpdateModuleCommandHandler _updateModuleCommandHandler;
    private GetLessonsByModuleIdQueryHandler _getLessonsByModuleIdQueryHandler;
    private GetModuleByIdQueryHandler _getModuleByIdQueryHandler;
    private GetModulesQueryHandler _getModulesQueryHandler;
    private CreateBlogCommandHandler _createBlogCommandHandler;
    private DisableBlogCommandHandler _disableBlogCommandHandler;
    private UpdateBlogCommandHandler _updateBlogCommandHandler;
    private GetBlogByIdQueryHandler _getBlogByIdQueryHandler;
    private GetBlogsQueryHandler _getBlogsQueryHandler;
    private GetCategoriesQueryHandler _getCategoriesQueryHandler;
    private GetDistrictsByCityIdQueryHandler _getDistrictsByCityIdQueryHandler;
    private GetCitiesQueryHandler _getCitiesQueryHandler;

    [TestInitialize]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _weeksRepositoryMock = new Mock<IWeekRepository>();
        _teacherLessonsRepoMock = new Mock<ITeacherLessonRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _schoolRepositoryMock = new Mock<ISchoolRepository>();
        _gradeRepositoryMock = new Mock<IGradeRepository>();
        _emailServiceMock = new Mock<IEmailService>();
        _mapperMock = new Mock<IMapper>();
        _uploadFileServiceMock = new Mock<IUploadFileService>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _lessonRepositoryMock = new Mock<ILessonsRepository>();
        _promptRepositoryMock = new Mock<IPromptRepository>();
        _teacherLessonRepoMock = new Mock<ITeacherLessonRepository>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _lessonHistoryRepoMock = new Mock<ILessonHistoryRepository>();
        _moduleRepoMock = new Mock<IModuleRepository>();
        _curriculumRepoMock = new Mock<ICurriculumRepository>();
        _bookRepoMock = new Mock<IBookRepository>();
    }

    [TestMethod]
    public async Task Handle_GetWeeksQueryHandler_ReturnsSuccessResponseWithMappedData()
    {
        _unitOfWorkMock.Setup(u => u.Weeks).Returns(_weeksRepositoryMock.Object);

        _getWeeksQueryHandler = new GetWeeksQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var weekEntities = new List<Week>
        {
            new Week { WeekId = 1 },
            new Week { WeekId = 2 }
        };

        var mappedResponse = new List<GetWeekResponse>
        {
        };

        _weeksRepositoryMock
            .Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Week, bool>>>(),
                It.IsAny<Func<IQueryable<Week>, IQueryable<Week>>>(),
                It.IsAny<bool>()))
            .ReturnsAsync(weekEntities.AsQueryable());

        _mapperMock
            .Setup(mapper => mapper.Map<List<GetWeekResponse>>(weekEntities))
            .Returns(mappedResponse);

        var query = new GetWeeksQuery();

        // Act
        var result = await _getWeeksQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.SUCCESS.GetDescription(), result.Message);
        CollectionAssert.AreEqual(mappedResponse, result.Data);
    }

    #region User
    [TestMethod]
    public async Task Handle_ReturnsMappedLessons_WhenTeacherExists()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonsRepoMock.Object);

        _getTeacherLessonsByIdQueryHandler = new GetTeacherLessonsByIdQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object);

        // Arrange
        var teacherId = 1;

        var teacherLessons = new List<TeacherLesson>
        {
            new TeacherLesson
            {
                UserId = teacherId,
                Prompt = new Prompt
                {
                    Lesson = new Lesson { Name = "Sample Lesson" }
                }
            }
        }.AsQueryable();

        var mappedLessons = new List<GetUserLessonsResponse>
        {
            new GetUserLessonsResponse { Lesson = "Sample Lesson" }
        };

        _teacherLessonsRepoMock
            .Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<TeacherLesson, bool>>>(),
                It.IsAny<Func<IQueryable<TeacherLesson>, IQueryable<TeacherLesson>>>(),
                true))
            .ReturnsAsync(teacherLessons);

        _mapperMock
            .Setup(m => m.Map<List<GetUserLessonsResponse>>(teacherLessons))
            .Returns(mappedLessons);

        var query = new GetTeacherLessonsByIdQuery(teacherId);

        // Act
        var result = await _getTeacherLessonsByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.SUCCESS.GetDescription(), result.Message);
        Assert.AreEqual(mappedLessons, result.Data);
    }

    [TestMethod]
    public async Task Handle_ReturnsMappedUser_WhenUserExists()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _getUserByIdQueryHandler = new GetUserByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var userId = 1;
        var user = new User
        {
            UserId = userId,
            Role = new Role(),
            Manager = new User(),
            School = new School(),
            Grade = new Grade(),
            Ward = new Ward
            {
                District = new District
                {
                    City = new City()
                }
            }
        };

        var users = new List<User> { user }.AsQueryable();

        _userRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(users);

        var mapped = new GetUserResponse { UserId = userId };

        _mapperMock
            .Setup(m => m.Map<GetUserResponse>(user))
            .Returns(mapped);

        var query = new GetUserByIdQuery(userId);

        // Act
        var result = await _getUserByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(mapped, result.Data);
        Assert.AreEqual(ResponseCode.SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_ThrowsException_WhenUserNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _getUserByIdQueryHandler = new GetUserByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var userId = 0;

        _userRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new List<User>().AsQueryable());

        var query = new GetUserByIdQuery(userId);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _getUserByIdQueryHandler.Handle(query, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ReturnsMappedPaginatedUsers()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _getUsersQueryHandler = new GetUsersQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var query = new GetUsersQuery
        (
            "john",
            "name",
            "asc",
            null,
            null,
            null,
            1,
            1,
            10
        );

        var paginatedEntities = new PaginatedList<User>(
            new List<User> { new User { UserId = 1, Fullname = "John Doe" } },
            currentPage: 1,
            pageSize: 1,
            totalRecords: 10);

        var mappedPaginatedList = new PaginatedList<GetUserResponse>(
            new List<GetUserResponse> { new GetUserResponse { Fullname = "John Doe" } },
            currentPage: 1,
            pageSize: 1,
            totalRecords: 10);

        _userRepositoryMock
            .Setup(repo => repo.PaginatedListAsync(
                query.SearchTerm,
                query.SortColumn,
                query.SortOrder,
                query.RoleId,
                query.GradeId,
                query.SchoolId,
                query.IsActive,
                query.Page,
                query.PageSize))
            .ReturnsAsync(paginatedEntities);

        _mapperMock
            .Setup(m => m.Map<PaginatedList<GetUserResponse>>(paginatedEntities))
            .Returns(mappedPaginatedList);

        // Act
        var result = await _getUsersQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.SUCCESS.GetDescription(), result.Message);
        Assert.AreEqual(mappedPaginatedList, result.Data);
    }

    [TestMethod]
    public async Task Handle_ReturnsMappedUserUpdateResponse_WhenUserExists()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _getUserUpdateByIdQueryHandler = new GetUserUpdateByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var userId = 1;
        var user = new User
        {
            UserId = userId,
            Role = new Role(),
            Manager = new User(),
            School = new School(),
            Grade = new Grade(),
            Gender = Domain.Common.Gender.Male,
            WardId = 1
        };

        var users = new List<User> { user }.AsQueryable();

        _userRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(users);

        var mapped = new GetUserUpdateResponse { UserId = userId };

        _mapperMock
            .Setup(m => m.Map<GetUserUpdateResponse>(user))
            .Returns(mapped);

        var query = new GetUserUpdateByIdQuery(userId);

        // Act
        var result = await _getUserUpdateByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(mapped, result.Data);
        Assert.AreEqual(ResponseCode.SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_UserUpdateResponseThrowsException_WhenUserNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _getUserUpdateByIdQueryHandler = new GetUserUpdateByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var userId = 0;

        _userRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new List<User>().AsQueryable());

        var query = new GetUserUpdateByIdQuery(userId);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _getUserUpdateByIdQueryHandler.Handle(query, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ValidRequest_ChangesPasswordSuccessfully()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _changePasswordCommandHandler = new ChangePasswordCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var user = new User { UserId = 1, Username = "john", Email = "john@example.com", PasswordHash = "old" };
        var queryableUser = new List<User> { user }.AsQueryable();

        _userRepositoryMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(queryableUser);

        _userRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(u => u.CompleteAsync())
            .Returns(Task.CompletedTask);

        var command = new ChangePasswordCommand
        (
            "john",
            "newpassword123",
            "newpassword123"
        );

        // Act
        var result = await _changePasswordCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.UPDATED_SUCCESS.GetDescription(), result.Message);
        _userRepositoryMock.Verify(r => r.UpdateAsync(It.Is<User>(u => u.PasswordHash == "newpassword123")), Times.Once);
    }

    [TestMethod]
    public async Task Handle_UserNotFound_ThrowsValidationException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _changePasswordCommandHandler = new ChangePasswordCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        _userRepositoryMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new List<User>().AsQueryable());

        var command = new ChangePasswordCommand
        (
            "notfound",
            "any",
            "any"
        );

        // Act
        await Assert.ThrowsExceptionAsync<ValidationException>(() => _changePasswordCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_InvalidPassword_ThrowsValidationException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _changePasswordCommandHandler = new ChangePasswordCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var user = new User { Username = "test", Email = "test@example.com" };
        var queryableUser = new List<User> { user }.AsQueryable();

        _userRepositoryMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(queryableUser);

        var command = new ChangePasswordCommand
        (
            "test",
            "short",
            "short"
        );

        // Act
        await Assert.ThrowsExceptionAsync<ValidationException>(() => _changePasswordCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ValidOtpAndPasswordsMatch_ResetsPassword()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _checkOtpCommandHandler = new CheckOtpCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var user = new User { PasswordHash = "old", ResetPasswordOtp = "123456" };

        _userRepositoryMock
            .Setup(r => r.ResetPasswordAsync("123456"))
            .ReturnsAsync(user);

        _userRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(u => u.CompleteAsync())
            .Returns(Task.CompletedTask);

        var command = new CheckOtpCommand
        (
             "123456",
             "newsecurepass",
             "newsecurepass"
        );

        // Act
        var result = await _checkOtpCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual("Reset password successfully!", result.Data);
        _userRepositoryMock.Verify(r => r.UpdateAsync(It.Is<User>(u => u.PasswordHash == "newsecurepass" && u.ResetPasswordOtp == null)), Times.Once);
    }

    [TestMethod]
    public async Task Handle_InvalidOtp_ThrowsValidationException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _checkOtpCommandHandler = new CheckOtpCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        _userRepositoryMock
            .Setup(r => r.ResetPasswordAsync("wrongOtp"))
            .ReturnsAsync((User)null);

        var command = new CheckOtpCommand
        (
            "wrongOtp",
            "pass",
            "pass"
        );

        // Act
        await Assert.ThrowsExceptionAsync<ValidationException>(() => _checkOtpCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_PasswordsDoNotMatch_ThrowsValidationException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);

        _checkOtpCommandHandler = new CheckOtpCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var user = new User { ResetPasswordOtp = "123456" };

        _userRepositoryMock
            .Setup(r => r.ResetPasswordAsync("123456"))
            .ReturnsAsync(user);

        var command = new CheckOtpCommand
        (
            "123456",
            "abc123",
            "xyz123"
        );

        // Act
        await Assert.ThrowsExceptionAsync<ValidationException>(() => _checkOtpCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ValidRequest_CreatesUserAndSendsEmail()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);


        _createUserCommandHandler = new CreateUserCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);
        // Arrange
        var command = new CreateUserCommand
        (
            "newuser",
            "123456",
            "newuser@example.com",
            2,
            1,
            1
        );

        var validatorMock = new Mock<CreateUserCommandValidator>(_unitOfWorkMock.Object);
        // validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
        //     .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _userRepositoryMock.Setup(r => r.CheckGradeManagerAsync(command.RoleId, command.GradeId))
            .ReturnsAsync(-1);

        _userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(new User());

        _roleRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Role { RoleId = 1, RoleName = "role" });
        _schoolRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new School());
        _gradeRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Grade());

        _emailServiceMock.Setup(e => e.SendEmailAsync(It.IsAny<MailRequest>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var response = await _createUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(response);
        Assert.AreEqual((int)ResponseCode.CREATED_SUCCESS, response.Code);
    }

    [TestMethod]
    public async Task Handle_ManagerAlreadyExists_ThrowsValidationException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);

        _createUserCommandHandler = new CreateUserCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);
        var command = new CreateUserCommand
        (
             "newuser",
             "123456",
             "newuser@example.com",
             2,
             1,
             1
        );

        var validatorMock = new Mock<CreateUserCommandValidator>(_unitOfWorkMock.Object);
        // validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
        //     .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        _roleRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Role { RoleId = 1, RoleName = "role" });
        _schoolRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new School());
        _gradeRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Grade());

        _userRepositoryMock.Setup(r => r.CheckGradeManagerAsync(command.RoleId, command.GradeId))
            .ReturnsAsync(1);

        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
        _createUserCommandHandler.Handle(command, CancellationToken.None));
    }


    [TestMethod]
    public async Task Handle_ValidRequest_ShouldCreateUserSuccessfully()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);

        _createUserCommandHandler = new CreateUserCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);

        var validatorMock = new Mock<CreateUserCommandValidator>(_unitOfWorkMock.Object);
        // validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
        //     .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        _roleRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Role { RoleId = 1, RoleName = "role" });
        _schoolRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new School());
        _gradeRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Grade());

        _createUserCommandHandler = new CreateUserCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);

        // Arrange
        var command = new CreateUserCommand
        (
             "newuser",
             "123456",
             "newuser@example.com",
             2,
             1,
             1
        );

        var validator = new CreateUserCommandValidator(_unitOfWorkMock.Object);
        // var validationResult = await validator.ValidateAsync(command);

        _userRepositoryMock.Setup(r => r.CheckGradeManagerAsync(2, 1)).ReturnsAsync(0);
        _userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(new User());

        _emailServiceMock.Setup(e => e.SendEmailAsync(It.IsAny<MailRequest>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
        _createUserCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_InvalidValidator_ShouldThrowValidationException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);

        _createUserCommandHandler = new CreateUserCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);

        _roleRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Role { RoleId = 1, RoleName = "role" });
        _schoolRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new School());
        _gradeRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Grade());

        // Arrange
        var command = new CreateUserCommand
        (
            "",
            "pass",
            "test@mail.com",
            2,
            1,
            1
        );

        // Act
        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
        _createUserCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ManagerAlreadyExists_ShouldThrowValidationException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);

        _createUserCommandHandler = new CreateUserCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);

        _roleRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Role { RoleId = 1, RoleName = "role" });
        _schoolRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new School());
        _gradeRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Grade());

        // Arrange
        var command = new CreateUserCommand
        (
            "test",
            "pass",
            "test@mail.com",
            2,
            1,
            1
        );

        var validator = new CreateUserCommandValidator(_unitOfWorkMock.Object);
        await validator.ValidateAsync(command);

        _userRepositoryMock.Setup(r => r.CheckGradeManagerAsync(2, 1)).ReturnsAsync(1); // Manager exists

        // Act
        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
        _createUserCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ViceManagerAlreadyExists_ShouldThrowValidationException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);

        _createUserCommandHandler = new CreateUserCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);

        _roleRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Role { RoleId = 1, RoleName = "role" });
        _schoolRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new School());
        _gradeRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Grade());

        // Arrange
        var command = new CreateUserCommand
        (
            "test",
            "pass",
            "test@mail.com",
            2,
            1,
            1
        );

        _userRepositoryMock.Setup(r => r.CheckGradeManagerAsync(2, 1)).ReturnsAsync(0); // Vice manager exists

        // Act
        await Assert.ThrowsExceptionAsync<ValidationException>(() =>
        _createUserCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_UserIsActive_ShouldDisableUser()
    {
        // Arrange
        var user = new User { UserId = 1, IsActive = true };
        var command = new DisableUserCommand(1);

        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);

        _disableUserCommandHandler = new DisableUserCommandHandler(_unitOfWorkMock.Object);

        _userRepositoryMock.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), null
        , null, true))
            .ReturnsAsync(new List<User> { user }.AsQueryable());
        _userRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var response = await _disableUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.DISABLED_SUCCESS, response.Code);
        Assert.IsFalse(user.IsActive);
    }

    [TestMethod]
    public async Task Handle_UserIsInactive_ShouldEnableUser()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);

        _disableUserCommandHandler = new DisableUserCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var user = new User { UserId = 2, IsActive = false };
        var command = new DisableUserCommand(2);

        _userRepositoryMock.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), null
        , null, true))
            .ReturnsAsync(new List<User> { user }.AsQueryable());
        _userRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var response = await _disableUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.DISABLED_SUCCESS, response.Code);
        Assert.IsTrue(user.IsActive);
    }

    [TestMethod]
    public async Task Handle_UserNotFound_ShouldThrowApiException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Roles).Returns(_roleRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Schools).Returns(_schoolRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);

        _disableUserCommandHandler = new DisableUserCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var command = new DisableUserCommand(3);

        _userRepositoryMock.Setup(r => r.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), null
        , null, true))
            .ReturnsAsync(new List<User>().AsQueryable());

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() =>
        _disableUserCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ValidEmail_ShouldSendOtpAndReturnSuccessResponse()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _sendEmailCommandHandler = new SendEmailCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);
        // Arrange
        var fakeUser = new User { Email = "test@example.com" };

        _unitOfWorkMock.Setup(u => u.Users.SendOtpAsync(It.IsAny<string>()))
            .ReturnsAsync(fakeUser);

        _unitOfWorkMock.Setup(u => u.Users.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);
        _emailServiceMock.Setup(e => e.SendEmailAsync(It.IsAny<MailRequest>())).Returns(Task.CompletedTask);

        var command = new SendEmailCommand("test@example.com");

        // Act
        var result = await _sendEmailCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
    }

    [TestMethod]
    public async Task Handle_EmailNotFound_ShouldThrowApiException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _sendEmailCommandHandler = new SendEmailCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);
        // Arrange
        _unitOfWorkMock.Setup(u => u.Users.SendOtpAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null);

        var command = new SendEmailCommand("nonexistent@example.com");

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() =>
               _sendEmailCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_SendEmailFails_ShouldThrowException()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _sendEmailCommandHandler = new SendEmailCommandHandler(_unitOfWorkMock.Object, _emailServiceMock.Object);
        // Arrange
        User? fakeUser = null;

        _unitOfWorkMock.Setup(u => u.Users.SendOtpAsync(It.IsAny<string>()))
            .ReturnsAsync(fakeUser);

        _emailServiceMock.Setup(e => e.SendEmailAsync(It.IsAny<MailRequest>()))
            .ThrowsAsync(new Exception("SMTP failure"));

        _unitOfWorkMock.Setup(u => u.Users.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        var command = new SendEmailCommand("fail@example.com");

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() =>
       _sendEmailCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldUpdateUserSuccessfully()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _updateUserCommandHandler = new UpdateUserCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        var userId = 1;
        var existingUser = new User { UserId = userId, IsActive = false };
        var request = new UpdateUserRequest
        (
            "Updated Name",
            "updated@email.com",
            "0987654321",
            new DateOnly(1990, 1, 1),
            Gender.Male,
            "Male"
        );

        var command = new UpdateUserCommand
        (
            userId,
            request
        );

        _userRepositoryMock.Setup(r => r.GetAsync(
            It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
            It.IsAny<Func<IQueryable<User>, IQueryable<User>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<User> { existingUser }.AsQueryable());
        var a = new ValidationResult();
        var validatorMock = new Mock<FluentValidation.IValidator<UpdateUserRequest>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<UpdateUserRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(a);

        _userRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _updateUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowValidationException_WhenUserNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _updateUserCommandHandler = new UpdateUserCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        var command = new UpdateUserCommand
        (
             1,
             new UpdateUserRequest("fullname",
            "email",
            "phonenumber",
            DateOnly.MinValue,
            Gender.Male,
            "address")
        );

        _userRepositoryMock.Setup(r => r.GetAsync(
            It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
            It.IsAny<Func<IQueryable<User>, IQueryable<User>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<User>().AsQueryable());

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            await _updateUserCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrowValidationException_WhenValidationFails()
    {
        _unitOfWorkMock.Setup(u => u.Users).Returns(_userRepositoryMock.Object);
        _updateUserCommandHandler = new UpdateUserCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        var userId = 1;
        var existingUser = new User { UserId = userId };
        var request = new UpdateUserRequest
        (
            "",
            "",
            "",
            DateOnly.MinValue,
            Gender.Male,
            ""
        );

        var command = new UpdateUserCommand
        (
            userId,
            request
        );

        _userRepositoryMock.Setup(r => r.GetAsync(
            It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
            It.IsAny<Func<IQueryable<User>, IQueryable<User>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<User> { existingUser }.AsQueryable());

        var validator = new UpdateUserCommandValidator(_unitOfWorkMock.Object);
        await validator.ValidateAsync(request);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            await _updateUserCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldUploadProfileImageSuccessfully()
    {
        // Arrange
        var command = new UploadProfileImgCommand(MockFile("test.jpg", 1024 * 1024));
        var userId = "1";
        var user = new User { UserId = 1 };

        _currentUserServiceMock.Setup(x => x.CurrentPrincipal).Returns(userId);
        _unitOfWorkMock.Setup(x => x.Users.GetAsync(
            It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
            It.IsAny<Func<IQueryable<User>, IQueryable<User>>>(),
            It.IsAny<bool>()
        ))
            .ReturnsAsync(new List<User> { user }.AsQueryable());
        _uploadFileServiceMock.Setup(x => x.CloudinaryStorage(command.File)).ReturnsAsync("http://image.url");
        _unitOfWorkMock.Setup(x => x.Users.UpdateAsync(user)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        var handler = new UploadProfileImgCommandHandler(_unitOfWorkMock.Object, _uploadFileServiceMock.Object, _currentUserServiceMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
    }

    [TestMethod]
    [ExpectedException(typeof(ApiException))]
    public async Task Handle_ShouldThrowException_WhenFileExtensionInvalid()
    {
        var command = new UploadProfileImgCommand(MockFile("file.txt", 1024));
        var handler = new UploadProfileImgCommandHandler(_unitOfWorkMock.Object, _uploadFileServiceMock.Object, _currentUserServiceMock.Object);

        await handler.Handle(command, CancellationToken.None);
    }

    [TestMethod]
    [ExpectedException(typeof(ApiException))]
    public async Task Handle_ShouldThrowException_WhenFileTooLarge()
    {
        var command = new UploadProfileImgCommand(MockFile("file.jpg", 6 * 1024 * 1024));
        var handler = new UploadProfileImgCommandHandler(_unitOfWorkMock.Object, _uploadFileServiceMock.Object, _currentUserServiceMock.Object);

        await handler.Handle(command, CancellationToken.None);
    }

    [TestMethod]
    [ExpectedException(typeof(ApiException))]
    public async Task Handle_ShouldThrowException_WhenCurrentUserNull()
    {
        var command = new UploadProfileImgCommand(MockFile("test.jpg", 1024 * 1024));
        _currentUserServiceMock.Setup(x => x.CurrentPrincipal).Returns((string)null);

        var handler = new UploadProfileImgCommandHandler(_unitOfWorkMock.Object, _uploadFileServiceMock.Object, _currentUserServiceMock.Object);

        await handler.Handle(command, CancellationToken.None);
    }

    [TestMethod]
    [ExpectedException(typeof(ApiException))]
    public async Task Handle_ShouldThrowException_WhenUserNotFound()
    {
        var command = new UploadProfileImgCommand(MockFile("test.jpg", 1024 * 1024));
        _currentUserServiceMock.Setup(x => x.CurrentPrincipal).Returns("1");
        _unitOfWorkMock.Setup(x => x.Users.GetAsync(
            It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
            It.IsAny<Func<IQueryable<User>, IQueryable<User>>>(),
            It.IsAny<bool>()
        ))
            .ReturnsAsync(new List<User>().AsQueryable());

        var handler = new UploadProfileImgCommandHandler(_unitOfWorkMock.Object, _uploadFileServiceMock.Object, _currentUserServiceMock.Object);

        await handler.Handle(command, CancellationToken.None);
    }

    private IFormFile MockFile(string fileName, long length)
    {
        var fileMock = new Mock<IFormFile>();
        var content = "fake file";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        fileMock.Setup(f => f.FileName).Returns(fileName);
        fileMock.Setup(f => f.Length).Returns(length);
        fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
        return fileMock.Object;
    }

    #endregion

    #region Lesson
    [TestMethod]
    public async Task Handle_GetLessonByIdQueryHandler_ReturnsSuccessResponseWithLessonDetail()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _getLessonByIdQueryHandler = new GetLessonByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var lessonId = 1;

        var lesson = new Lesson
        {
            LessonId = lessonId,
            Name = "Lesson A",
            Description = "Description A",
            TotalPeriods = 10,
            LessonType = new LessonType { LessonTypeName = "Type A" },
            Note = new Note { Description = "Note A" },
            Week = new Week { WeekNumber = 5 },
            Module = new Module
            {
                Name = "Module A",
                Grade = new Grade { GradeNumber = 12 }
            }
        };

        var lessonsQueryable = new List<Lesson> { lesson }.AsQueryable();

        _getLessonByIdQueryHandler = new GetLessonByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);


        _lessonRepositoryMock
            .Setup(repo => repo.GetAsync(
                It.IsAny<Expression<Func<Lesson, bool>>>()))
            .ReturnsAsync(lessonsQueryable);

        var query = new GetLessonByIdQuery(lessonId);

        // Act
        var result = await _getLessonByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.SUCCESS.GetDescription(), result.Message);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(lessonId, result.Data.LessonId);
        Assert.AreEqual("Lesson A", result.Data.Name);
        Assert.AreEqual("Description A", result.Data.Description);
        Assert.AreEqual(10, result.Data.TotalPeriods);
        Assert.AreEqual("Type A", result.Data.LessonType);
        Assert.AreEqual("Note A", result.Data.Note);
        Assert.AreEqual("Module A", result.Data.Module);
        Assert.AreEqual(12, result.Data.GradeNumber);
    }

    [TestMethod]
    public async Task Handle_GetLessonsQueryHandler_ReturnsPaginatedLessonResponses()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _getLessonsQueryHandler = new GetLessonsQueryHandler(_unitOfWorkMock.Object);

        // Arrange
        var pageNumber = 1;
        var pageSize = 10;

        var lessons = new List<Lesson>
    {
        new Lesson
        {
            LessonId = 1,
            Name = "Math",
            Description = "Basic Math",
            TotalPeriods = 5,
            LessonType = new LessonType { LessonTypeName = "Lecture" },
            Note = new Note { Description = "Note 1" },
            Week = new Week { WeekNumber = 1 },
            Module = new Module
            {
                Name = "Algebra",
                Grade = new Grade { GradeNumber = 10 }
            }
        }
    };

        var paginationResult = new BasePaginationEntity<Lesson>
        {
            Data = lessons,
            Total = lessons.Count
        };

        _lessonRepositoryMock
            .Setup(repo => repo.PaginationAsync(
                pageNumber,
                pageSize,
                It.IsAny<Expression<Func<Lesson, bool>>>(),
                It.IsAny<Func<IQueryable<Lesson>, IIncludableQueryable<Lesson, object>>>(),
                It.IsAny<Func<IQueryable<Lesson>, IOrderedQueryable<Lesson>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginationResult);

        var query = new GetLessonsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var result = await _getLessonsQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.SUCCESS.GetDescription(), result.Message);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(pageNumber, result.Data.Page);
        Assert.AreEqual(pageSize, result.Data.PageSize);
        Assert.AreEqual(1, result.Data.TotalSize);
        Assert.AreEqual(1, result.Data.TotalPage);
        Assert.AreEqual(1, result.Data.Items.Count);

        var lessonResponse = result.Data.Items.First();
        Assert.AreEqual(1, lessonResponse.LessonId);
        Assert.AreEqual("Math", lessonResponse.Name);
        Assert.AreEqual("Basic Math", lessonResponse.Description);
        Assert.AreEqual(5, lessonResponse.TotalPeriods);
        Assert.AreEqual("Lecture", lessonResponse.LessonType);
        Assert.AreEqual("Note 1", lessonResponse.Note);
        Assert.AreEqual("Algebra", lessonResponse.Module);
        Assert.AreEqual(10, lessonResponse.GradeNumber);
    }

    [TestMethod]
    public async Task Handle_GetPromptByIdQueryHandler_ReturnsMappedPromptResponse()
    {
        _unitOfWorkMock.Setup(u => u.Prompts).Returns(_promptRepositoryMock.Object);
        _getPromptByIdQueryHandler = new GetPromptByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var lessonId = 1;

        var prompt = new Prompt
        {
            PromptId = 10,
            LessonId = lessonId,
        };

        var promptList = new List<Prompt> { prompt }.AsQueryable();

        _promptRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Prompt, bool>>>()))
            .ReturnsAsync(promptList);

        var mappedResponse = new GetPromptResponse
        {
            PromptId = prompt.PromptId,
        };

        _mapperMock
            .Setup(m => m.Map<GetPromptResponse>(prompt))
            .Returns(mappedResponse);

        var query = new GetPromptByIdQuery(lessonId);

        // Act
        var result = await _getPromptByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.SUCCESS.GetDescription(), result.Message);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(prompt.PromptId, result.Data.PromptId);
    }

    [TestMethod]
    public async Task Handle_CreateLessonCommandHandler_ReturnsCreatedResponse()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _createLessonCommandHandler = new CreateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var createRequest = new CreateLessonRequest
        {
            Name = "New Lesson",
            Description = "Description",
            TotalPeriods = 5,
            LessonTypeId = 1,
            NoteId = 1,
            ModuleId = 1
        };

        var command = new CreateLessonCommand(createRequest);

        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>()))
            .Returns(true);

        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>()))
            .Returns(true);

        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>()))
            .Returns(true);

        _lessonRepositoryMock.Setup(repo => repo.GetLastIdLesson())
            .Returns(10);

        _lessonRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Lesson>()))
            .ReturnsAsync((Lesson l) => l);

        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _createLessonCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.CREATED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.CREATED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_CreateLessonCommandHandler_ThrowLessonType()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _createLessonCommandHandler = new CreateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var createRequest = new CreateLessonRequest
        {
            Name = "New Lesson",
            Description = "Description",
            TotalPeriods = 5,
            LessonTypeId = 1,
            ModuleId = 1
        };

        var command = new CreateLessonCommand(createRequest);

        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>()))
            .Returns(false);

        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>()))
            .Returns(true);

        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>()))
            .Returns(true);

        _lessonRepositoryMock.Setup(repo => repo.GetLastIdLesson())
            .Returns(10);

        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _createLessonCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_CreateLessonCommandHandler_ThrowNote()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _createLessonCommandHandler = new CreateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var createRequest = new CreateLessonRequest
        {
            Name = "New Lesson",
            Description = "Description",
            TotalPeriods = 5,
            LessonTypeId = 1,
            ModuleId = 1
        };

        var command = new CreateLessonCommand(createRequest);

        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>()))
            .Returns(true);

        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>()))
            .Returns(false);

        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>()))
            .Returns(true);

        _lessonRepositoryMock.Setup(repo => repo.GetLastIdLesson())
            .Returns(10);

        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _createLessonCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_CreateLessonCommandHandler_ThrowModule()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _createLessonCommandHandler = new CreateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var createRequest = new CreateLessonRequest
        {
            Name = "New Lesson",
            Description = "Description",
            TotalPeriods = 5,
            LessonTypeId = 1,
            NoteId = 1,
            ModuleId = 1
        };

        var command = new CreateLessonCommand(createRequest);

        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>()))
            .Returns(true);

        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>()))
            .Returns(true);

        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>()))
            .Returns(false);

        _lessonRepositoryMock.Setup(repo => repo.GetLastIdLesson())
            .Returns(10);

        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _createLessonCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_DeleteLessonCommandHandler_ReturnsDeletedResponse()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _deleteLessonCommandHandler = new DeleteLessonCommandHandler(_unitOfWorkMock.Object);

        var command = new DeleteLessonCommand(1);

        // Arrange
        var lessonId = 1;
        var existingLesson = new Lesson { LessonId = lessonId };

        _lessonRepositoryMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<Lesson, bool>>>(), null, null, true))
            .ReturnsAsync(new List<Lesson> { existingLesson }.AsQueryable());

        _lessonRepositoryMock
            .Setup(r => r.UpdateAsync(existingLesson))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(u => u.CompleteAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _deleteLessonCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.DELETED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.DELETED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_UpdateIsApprovedLessonCommandHandler_ReturnsUpdatedResponse()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _updateIsApprovedLessonCommandHandler = new UpdateIsApprovedLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var lessonId = 1;
        var lesson = new Lesson
        {
            LessonId = lessonId,
        };

        var command = new UpdateIsApprovedLessonCommand
        (
            lessonId,
            new UpdateIsApprovedRequest
            {
                IsApproved = true,
                DisapprovedReason = null
            }
        );

        _lessonRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Lesson, bool>>>(), null, null, true))
            .ReturnsAsync(new List<Lesson> { lesson }.AsQueryable());

        _lessonRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Lesson>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(u => u.CompleteAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _updateIsApprovedLessonCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.UPDATED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_UpdateLessonCommandHandler_ReturnsUpdatedResponse()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _updateLessonCommandHandler = new UpdateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var lessonId = 1;
        var lesson = new Lesson
        {
            LessonId = lessonId,
            Name = "Old Name",
            Description = "Old Description",
            TotalPeriods = 10,
            LessonTypeId = 1,
            NoteId = 1,
            WeekId = 1,
            ModuleId = 1
        };

        var updateRequest = new UpdateLessonRequest
        {
            Name = "Updated Name",
            Description = "Updated Description",
            TotalPeriods = 12,
            LessonTypeId = 1,
            NoteId = 1,
        };

        var command = new UpdateLessonCommand
        (lessonId,
           updateRequest
        );

        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>())).Returns(true);
        _lessonRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Lesson, bool>>>(), null, null, true))
            .ReturnsAsync(new List<Lesson> { lesson }.AsQueryable());

        _lessonRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Lesson>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _updateLessonCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.UPDATED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_UpdateLessonCommandHandler_ThrowLessonType()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _updateLessonCommandHandler = new UpdateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var lessonId = 1;
        var lesson = new Lesson
        {
            LessonId = lessonId,
            Name = "Old Name",
            Description = "Old Description",
            TotalPeriods = 10,
            LessonTypeId = 1,
            NoteId = 1,
            WeekId = 1,
            ModuleId = 1
        };

        var updateRequest = new UpdateLessonRequest
        {
            Name = "Updated Name",
            Description = "Updated Description",
            TotalPeriods = 12,
            LessonTypeId = 1,
            NoteId = 1,
        };

        var command = new UpdateLessonCommand
        (lessonId,
           updateRequest
        );

        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>())).Returns(false);
        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>())).Returns(true);

        _lessonRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Lesson, bool>>>(), null, null, true))
            .ReturnsAsync(new List<Lesson> { lesson }.AsQueryable());

        _lessonRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Lesson>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _updateLessonCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_UpdateLessonCommandHandler_ThrowNote()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _updateLessonCommandHandler = new UpdateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var lessonId = 1;
        var lesson = new Lesson
        {
            LessonId = lessonId,
            Name = "Old Name",
            Description = "Old Description",
            TotalPeriods = 10,
            LessonTypeId = 1,
            NoteId = 1,
            WeekId = 1,
            ModuleId = 1
        };

        var updateRequest = new UpdateLessonRequest
        {
            Name = "Updated Name",
            Description = "Updated Description",
            TotalPeriods = 12,
            LessonTypeId = 1,
            NoteId = 1,
        };

        var command = new UpdateLessonCommand
        (lessonId,
           updateRequest
        );
        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>())).Returns(false);
        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>())).Returns(true);

        _lessonRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Lesson, bool>>>(), null, null, true))
            .ReturnsAsync(new List<Lesson> { lesson }.AsQueryable());

        _lessonRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Lesson>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _updateLessonCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_UpdateLessonCommandHandler_ThrowModule()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _updateLessonCommandHandler = new UpdateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var lessonId = 1;
        var lesson = new Lesson
        {
            LessonId = lessonId,
            Name = "Old Name",
            Description = "Old Description",
            TotalPeriods = 10,
            LessonTypeId = 1,
            NoteId = 1,
            WeekId = 1,
            ModuleId = 1
        };

        var updateRequest = new UpdateLessonRequest
        {
            Name = "Updated Name",
            Description = "Updated Description",
            TotalPeriods = 12,
            LessonTypeId = 1,
            NoteId = 1,
        };

        var command = new UpdateLessonCommand
        (lessonId,
           updateRequest
        );

        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>())).Returns(false);

        _lessonRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Lesson, bool>>>(), null, null, true))
            .ReturnsAsync(new List<Lesson> { lesson }.AsQueryable());

        _lessonRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Lesson>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _updateLessonCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_UpdateLessonCommandHandler_ThrowNotFoundLesson()
    {
        _unitOfWorkMock.Setup(u => u.Lessons).Returns(_lessonRepositoryMock.Object);
        _updateLessonCommandHandler = new UpdateLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var lessonId = 1;
        var updateRequest = new UpdateLessonRequest
        {
            Name = "Updated Name",
            Description = "Updated Description",
            TotalPeriods = 12,
            LessonTypeId = 1,
            NoteId = 1,
        };

        var command = new UpdateLessonCommand
        (lessonId,
           updateRequest
        );

        _unitOfWorkMock.Setup(u => u.LessonTypes.Any(It.IsAny<Expression<Func<LessonType, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(u => u.Notes.Any(It.IsAny<Expression<Func<Note, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(u => u.Modules.Any(It.IsAny<Expression<Func<Module, bool>>>())).Returns(true);

        _lessonRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Lesson, bool>>>(), null, null, true))
            .ReturnsAsync(new List<Lesson>().AsQueryable());

        _lessonRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Lesson>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        await Assert.ThrowsExceptionAsync<ApiException>(() => _updateLessonCommandHandler.Handle(command, CancellationToken.None));
    }
    #endregion

    #region TeacherLesson
    [TestMethod]
    public async Task Handle_GetTeacherLessonById_ReturnsMappedResponse()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _getTeacherLessonByIdQueryHandler = new GetTeacherLessonByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var lessonPlanId = 1;
        var teacherLesson = new TeacherLesson
        {
            LessonPlanId = lessonPlanId,
            Prompt = new Prompt
            {
                Lesson = new Lesson
                {
                    Module = new Module()
                }
            },
            User = new User()
        };

        var query = new GetTeacherLessonByIdQuery(lessonPlanId);
        var expectedResponse = new GetDetailTeacherLessonResponse();

        _teacherLessonRepoMock
            .Setup(repo => repo.GetAsync(
                It.IsAny<Expression<Func<TeacherLesson, bool>>>()))
            .ReturnsAsync(new List<TeacherLesson> { teacherLesson }.AsQueryable());

        _mapperMock.Setup(m => m.Map<GetDetailTeacherLessonResponse>(teacherLesson)).Returns(expectedResponse);

        // Act
        var result = await _getTeacherLessonByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(expectedResponse, result.Data);
    }

    [TestMethod]
    public async Task Handle_ReturnsMappedPaginatedListOfTeacherLessonResponses()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _getTeacherLessonsQueryHandler = new GetTeacherLessonsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var request = new GetTeacherLessonsQuery
        (
            "",
            "Name",
            "asc",
            1,
            2,
            3,
            4,
            LessonStatus.Approved,
            1,
            10)
        ;

        var mockResult = new PaginatedList<TeacherLesson>
        (
            new List<TeacherLesson> { new TeacherLesson() },
            1,
            1,
            1)
        ;

        var mappedResult = new PaginatedList<GetTeacherLessonResponse>
        (
            new List<GetTeacherLessonResponse> { new GetTeacherLessonResponse() },
            1,
            1,
            1
        );

        _teacherLessonRepoMock.Setup(r => r.PaginatedListAsync(
            request.SearchTerm,
            request.SortColumn,
            request.SortOrder,
            request.ModuleId,
            request.LessonId,
            request.UserId,
            request.GradeId,
            request.Status,
            request.Page,
            request.PageSize)).ReturnsAsync(mockResult);

        _mapperMock.Setup(m => m.Map<PaginatedList<GetTeacherLessonResponse>>(mockResult)).Returns(mappedResult);

        // Act
        var result = await _getTeacherLessonsQueryHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(mappedResult, result.Data);
    }

    [TestMethod]
    public async Task Handle_ShouldCreateTeacherLesson_WhenNotBelongedToTeacher()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _createPendingTeacherLessonCommandHandler = new CreatePendingTeacherLessonCommandHandler(_unitOfWorkMock.Object, _dateTimeProviderMock.Object);

        // Arrange
        var request = new CreatePendingTeacherLessonCommand
        (
            "Warm-up",
            "Theory",
            "Coding",
            "Project",
            "Learn async",
            "Laptop",
            1,
            10
        );

        _teacherLessonRepoMock.Setup(r => r.IsBelongedToTeacherAsync(request.UserId, request.LessonId)).ReturnsAsync(false);
        _dateTimeProviderMock.Setup(p => p.UtcNow).Returns(new DateTime(2024, 1, 1));

        // Act
        var result = await _createPendingTeacherLessonCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        _teacherLessonRepoMock.Verify(r => r.AddAsync(It.Is<TeacherLesson>(
            t => t.UserId == request.UserId &&
                 t.PromptId == request.LessonId &&
                 t.Status == LessonStatus.Pending &&
                 t.CreatedAt == new DateTime(2024, 1, 1))), Times.Once);

        Assert.AreEqual((int)ResponseCode.CREATED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.CREATED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnCreatedSuccess_WhenTeacherLessonIsNew()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);

        _createTeacherLessonCommandHandler = new CreateTeacherLessonCommandHandler(
            _unitOfWorkMock.Object,
            _dateTimeProviderMock.Object
        );

        // Arrange
        var request = new CreateTeacherLessonCommand
        (
            "Introduction",
            "Theory",
            "Hands-on",
            "Discussion",
            "Understand topic",
            "Laptop",
            "5",
            1,
            5
        );

        var expectedTime = new DateTime(2024, 1, 1);

        _teacherLessonRepoMock
            .Setup(repo => repo.IsBelongedToTeacherAsync(request.UserId, request.LessonId))
            .ReturnsAsync(false);

        _dateTimeProviderMock
            .Setup(dp => dp.UtcNow)
            .Returns(expectedTime);

        // Act
        var result = await _createTeacherLessonCommandHandler.Handle(request, CancellationToken.None);

        // Assert

        Assert.AreEqual((int)ResponseCode.CREATED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.CREATED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_ShouldUpdateStatus_WhenTeacherLessonExists()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _updateStatusTeacherLessonCommandHandler = new UpdateStatusTeacherLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var teacherLesson = new TeacherLesson { LessonPlanId = 1, Status = LessonStatus.Draft };
        var request = new UpdateStatusTeacherLessonCommand
        (
            1,
            new UpdateStatusTeacherLessonRequest
            {
                Status = LessonStatus.Approved
            }
        );

        _teacherLessonRepoMock.Setup(r => r.GetAsync(
            It.IsAny<Expression<Func<TeacherLesson, bool>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IOrderedQueryable<TeacherLesson>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IQueryable<TeacherLesson>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<TeacherLesson> { teacherLesson }.AsQueryable());

        // Act
        var result = await _updateStatusTeacherLessonCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
        Assert.AreEqual(LessonStatus.Approved, teacherLesson.Status);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowException_WhenTeacherLessonNotFound()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _updateStatusTeacherLessonCommandHandler = new UpdateStatusTeacherLessonCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var request = new UpdateStatusTeacherLessonCommand
        (
            999,
            new UpdateStatusTeacherLessonRequest { Status = LessonStatus.Approved }
        );

        _teacherLessonRepoMock.Setup(r => r.GetAsync(
            It.IsAny<Expression<Func<TeacherLesson, bool>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IOrderedQueryable<TeacherLesson>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IQueryable<TeacherLesson>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<TeacherLesson>().AsQueryable());

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _updateStatusTeacherLessonCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrowException_WhenRejectedWithoutReason()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _updateStatusTeacherLessonCommandHandler = new UpdateStatusTeacherLessonCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        var teacherLesson = new TeacherLesson { LessonPlanId = 2, Status = LessonStatus.Pending };
        var request = new UpdateStatusTeacherLessonCommand
        (
            2,
            new UpdateStatusTeacherLessonRequest
            {
                Status = LessonStatus.Rejected,
                DisapprovedReason = null
            }
        );

        _teacherLessonRepoMock.Setup(r => r.GetAsync(
            It.IsAny<Expression<Func<TeacherLesson, bool>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IOrderedQueryable<TeacherLesson>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IQueryable<TeacherLesson>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<TeacherLesson> { teacherLesson }.AsQueryable());

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _updateStatusTeacherLessonCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrowException_WhenRejectedWithReason()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _updateStatusTeacherLessonCommandHandler = new UpdateStatusTeacherLessonCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        var teacherLesson = new TeacherLesson { LessonPlanId = 2, Status = LessonStatus.Pending };
        var request = new UpdateStatusTeacherLessonCommand
        (
            2,
            new UpdateStatusTeacherLessonRequest
            {
                Status = LessonStatus.Rejected,
                DisapprovedReason = "disapprovedreason"
            }
        );

        _teacherLessonRepoMock.Setup(r => r.GetAsync(
            It.IsAny<Expression<Func<TeacherLesson, bool>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IOrderedQueryable<TeacherLesson>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IQueryable<TeacherLesson>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<TeacherLesson> { teacherLesson }.AsQueryable());

        // Act & Assert
        var result = await _updateStatusTeacherLessonCommandHandler.Handle(request, CancellationToken.None);

        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
        Assert.AreEqual(LessonStatus.Rejected, teacherLesson.Status);
    }

    [TestMethod]
    public async Task Handle_ShouldClearDisapprovedReason_WhenStatusIsDraft()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _updateStatusTeacherLessonCommandHandler = new UpdateStatusTeacherLessonCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        var teacherLesson = new TeacherLesson
        {
            LessonPlanId = 3,
            Status = LessonStatus.Rejected,
            DisapprovedReason = "Old reason"
        };

        var request = new UpdateStatusTeacherLessonCommand
        (
            3,
            new UpdateStatusTeacherLessonRequest
            {
                Status = LessonStatus.Draft
            }
        );

        _teacherLessonRepoMock.Setup(r => r.GetAsync(
            It.IsAny<Expression<Func<TeacherLesson, bool>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IOrderedQueryable<TeacherLesson>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IQueryable<TeacherLesson>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<TeacherLesson> { teacherLesson }.AsQueryable());

        // Act
        await _updateStatusTeacherLessonCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual(string.Empty, teacherLesson.DisapprovedReason);
    }

    [TestMethod]
    public async Task Handle_ShouldUpdateLessonAndCreateHistory_WhenLessonExists()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.LessonHistories).Returns(_lessonHistoryRepoMock.Object);

        _updateTeacherLessonCommandHandler = new UpdateTeacherLessonCommandHandler(_unitOfWorkMock.Object, _dateTimeProviderMock.Object);

        // Arrange
        var teacherLesson = new TeacherLesson
        {
            LessonPlanId = 1,
            StartUp = "Old Start",
            Knowledge = "Old Knowledge",
            Goal = "Old Goal",
            SchoolSupply = "Old Supply",
            Practice = "Old Practice",
            Apply = "Old Apply"
        };

        var request = new UpdateTeacherLessonCommand
        (
            1,
            new UpdateTeacherLessonRequest
            {
                StartUp = "New Start",
                Knowledge = "New Knowledge",
                Goal = "New Goal",
                SchoolSupply = "New Supply",
                Practice = "New Practice",
                Apply = "New Apply"
            }
        );

        _teacherLessonRepoMock
            .Setup(repo => repo.GetAsync(
                It.IsAny<Expression<Func<TeacherLesson, bool>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IOrderedQueryable<TeacherLesson>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IQueryable<TeacherLesson>>>(),
            It.IsAny<bool>()
            ))
            .ReturnsAsync(new List<TeacherLesson> { teacherLesson }.AsQueryable());

        // Act
        var result = await _updateTeacherLessonCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
        Assert.AreEqual("New Start", teacherLesson.StartUp);
        Assert.AreEqual("New Knowledge", teacherLesson.Knowledge);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowException_WhenTeacherLessonNotFound1()
    {
        _unitOfWorkMock.Setup(u => u.TeacherLessons).Returns(_teacherLessonRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.LessonHistories).Returns(_lessonHistoryRepoMock.Object);

        _updateTeacherLessonCommandHandler = new UpdateTeacherLessonCommandHandler(_unitOfWorkMock.Object, _dateTimeProviderMock.Object);

        // Arrange
        var request = new UpdateTeacherLessonCommand
        (
            999,
            new UpdateTeacherLessonRequest
            {
                StartUp = "Start",
                Knowledge = "Knowledge",
                Goal = "Goal",
                SchoolSupply = "Supply",
                Practice = "Practice",
                Apply = "Apply"
            }
        );

        _teacherLessonRepoMock
            .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<TeacherLesson, bool>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IOrderedQueryable<TeacherLesson>>>(),
            It.IsAny<Func<IQueryable<TeacherLesson>, IQueryable<TeacherLesson>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<TeacherLesson>().AsQueryable());

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _updateTeacherLessonCommandHandler.Handle(request, CancellationToken.None));
    }
    #endregion

    #region Module
    [TestMethod]
    public async Task Handle_ShouldCreateModule_WhenValid()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Curriculums).Returns(_curriculumRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Books).Returns(_bookRepoMock.Object);

        _createModuleCommandHandler = new CreateModuleCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var request = new CreateModuleCommand
        (new CreateModuleRequest
        {
            Name = "Math",
            Desciption = "Basic math",
            Semester = 1,
            CurriculumId = 1,
            GradeId = 2,
            BookId = 3
        }
        );

        _curriculumRepoMock.Setup(x => x.Any(It.IsAny<Expression<Func<Curriculum, bool>>>())).Returns(true);
        _gradeRepositoryMock.Setup(x => x.Any(It.IsAny<Expression<Func<Grade, bool>>>())).Returns(true);
        _bookRepoMock.Setup(x => x.Any(It.IsAny<Expression<Func<Book, bool>>>())).Returns(true);
        _moduleRepoMock.Setup(x => x.GetAsync(
            It.IsAny<Expression<Func<Module, bool>>>(),
            It.IsAny<Func<IQueryable<Module>, IOrderedQueryable<Module>>>(),
            It.IsAny<Func<IQueryable<Module>, IQueryable<Module>>>(),
            It.IsAny<bool>()));
        _moduleRepoMock.Setup(x => x.AddAsync(It.IsAny<Module>())).ReturnsAsync(new Module());

        // Act
        var result = await _createModuleCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.CREATED_SUCCESS, result.Code);
        _moduleRepoMock.Verify(x => x.AddAsync(It.IsAny<Module>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenCurriculumNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Curriculums).Returns(_curriculumRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Books).Returns(_bookRepoMock.Object);

        _createModuleCommandHandler = new CreateModuleCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var request = new CreateModuleCommand
        (new CreateModuleRequest { CurriculumId = 1, GradeId = 1, BookId = 1 }
        );

        _curriculumRepoMock.Setup(x => x.Any(It.IsAny<Expression<Func<Curriculum, bool>>>())).Returns(false);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _createModuleCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenGradeNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Curriculums).Returns(_curriculumRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Books).Returns(_bookRepoMock.Object);

        _createModuleCommandHandler = new CreateModuleCommandHandler(_unitOfWorkMock.Object);

        var request = new CreateModuleCommand
        (new CreateModuleRequest { CurriculumId = 1, GradeId = 1, BookId = 1 }
        );

        _curriculumRepoMock.Setup(x => x.Any(It.IsAny<Expression<Func<Curriculum, bool>>>())).Returns(true);
        _gradeRepositoryMock.Setup(x => x.Any(It.IsAny<Expression<Func<Grade, bool>>>())).Returns(false);

        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _createModuleCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenBookNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Curriculums).Returns(_curriculumRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Books).Returns(_bookRepoMock.Object);

        _createModuleCommandHandler = new CreateModuleCommandHandler(_unitOfWorkMock.Object);

        var request = new CreateModuleCommand
        (new CreateModuleRequest { CurriculumId = 1, GradeId = 1, BookId = 1 }
        );

        _curriculumRepoMock.Setup(x => x.Any(It.IsAny<Expression<Func<Curriculum, bool>>>())).Returns(true);
        _gradeRepositoryMock.Setup(x => x.Any(It.IsAny<Expression<Func<Grade, bool>>>())).Returns(true);
        _bookRepoMock.Setup(x => x.Any(It.IsAny<Expression<Func<Book, bool>>>())).Returns(false);

        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _createModuleCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenModuleAlreadyExists()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Curriculums).Returns(_curriculumRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Grades).Returns(_gradeRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.Books).Returns(_bookRepoMock.Object);

        _createModuleCommandHandler = new CreateModuleCommandHandler(_unitOfWorkMock.Object);

        var request = new CreateModuleCommand
        (new CreateModuleRequest { Name = "Math", CurriculumId = 1, GradeId = 1, BookId = 1 }
        );

        _curriculumRepoMock.Setup(x => x.Any(It.IsAny<Expression<Func<Curriculum, bool>>>())).Returns(true);
        _gradeRepositoryMock.Setup(x => x.Any(It.IsAny<Expression<Func<Grade, bool>>>())).Returns(true);
        _bookRepoMock.Setup(x => x.Any(It.IsAny<Expression<Func<Book, bool>>>())).Returns(true);
        _moduleRepoMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Module, bool>>>())).ReturnsAsync(new List<Module> { new Module() }.AsQueryable());

        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _createModuleCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldSoftDeleteModule_WhenExists()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);

        _deleteModuleCommandHandler = new DeleteModuleCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var moduleId = new DeleteModuleCommand(1);
        var existingModule = new Module { ModuleId = 1 };

        _moduleRepoMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Module, bool>>>(),
            It.IsAny<Func<IQueryable<Module>, IOrderedQueryable<Module>>>(),
            It.IsAny<Func<IQueryable<Module>, IQueryable<Module>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Module> { existingModule }.AsQueryable());

        _moduleRepoMock.Setup(x => x.DeleteAsync(It.IsAny<Module>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _deleteModuleCommandHandler.Handle(moduleId, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.DELETED_SUCCESS, result.Code);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenModuleNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);

        _deleteModuleCommandHandler = new DeleteModuleCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var moduleId = new DeleteModuleCommand(99);

        _moduleRepoMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Module, bool>>>(),
            It.IsAny<Func<IQueryable<Module>, IOrderedQueryable<Module>>>(),
            It.IsAny<Func<IQueryable<Module>, IQueryable<Module>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Module>().AsQueryable());

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _deleteModuleCommandHandler.Handle(moduleId, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldUpdateModule_WhenAllDataValid()
    {
        _unitOfWorkMock.Setup(x => x.Modules).Returns(_moduleRepoMock.Object);

        _updateModuleCommandHandler = new UpdateModuleCommandHandler(_unitOfWorkMock.Object);

        // Arrange
        var command = new UpdateModuleCommand
        (1,
            new()
            {
                Name = "Updated Module",
                Desciption = "Updated Description"
            }
        );

        _unitOfWorkMock.Setup(x => x.Curriculums.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Curriculum, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(x => x.Grades.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Grade, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(x => x.Books.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>())).Returns(true);

        var existingModule = new Module { ModuleId = 1, Name = "abc" };
        _moduleRepoMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Module, bool>>>(),
            It.IsAny<Func<IQueryable<Module>, IOrderedQueryable<Module>>>(),
            It.IsAny<Func<IQueryable<Module>, IQueryable<Module>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Module> { existingModule }.AsQueryable());

        _moduleRepoMock.Setup(x => x.UpdateAsync(It.IsAny<Module>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _updateModuleCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
        _moduleRepoMock.Verify(x => x.UpdateAsync(It.Is<Module>(m => m.Name == "Updated Module")), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenCurriculumNotFoundWhenUpdate()
    {
        _unitOfWorkMock.Setup(x => x.Modules).Returns(_moduleRepoMock.Object);

        _updateModuleCommandHandler = new UpdateModuleCommandHandler(_unitOfWorkMock.Object);

        var command = new UpdateModuleCommand
       (1,
             new()
       );

        _unitOfWorkMock.Setup(x => x.Curriculums.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Curriculum, bool>>>())).Returns(false);

        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _updateModuleCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenGradeNotFoundWhenUpdate()
    {
        _unitOfWorkMock.Setup(x => x.Modules).Returns(_moduleRepoMock.Object);

        _updateModuleCommandHandler = new UpdateModuleCommandHandler(_unitOfWorkMock.Object);

        var command = new UpdateModuleCommand
        (1,
            new()
        );

        _unitOfWorkMock.Setup(x => x.Curriculums.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Curriculum, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(x => x.Grades.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Grade, bool>>>())).Returns(false);

        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _updateModuleCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenBookNotFoundWhenUpdate()
    {
        _unitOfWorkMock.Setup(x => x.Modules).Returns(_moduleRepoMock.Object);

        _updateModuleCommandHandler = new UpdateModuleCommandHandler(_unitOfWorkMock.Object);

        var command = new UpdateModuleCommand
        (1,
             new()
        );

        _unitOfWorkMock.Setup(x => x.Curriculums.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Curriculum, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(x => x.Grades.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Grade, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(x => x.Books.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>())).Returns(false);

        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _updateModuleCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrow_WhenModuleNotFoundWhenUpdate()
    {
        _unitOfWorkMock.Setup(x => x.Modules).Returns(_moduleRepoMock.Object);

        _updateModuleCommandHandler = new UpdateModuleCommandHandler(_unitOfWorkMock.Object);

        var command = new UpdateModuleCommand
       (100,
            new()
       );

        _unitOfWorkMock.Setup(x => x.Curriculums.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Curriculum, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(x => x.Grades.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Grade, bool>>>())).Returns(true);
        _unitOfWorkMock.Setup(x => x.Books.Any(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>())).Returns(true);

        _moduleRepoMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Module, bool>>>(),
            It.IsAny<Func<IQueryable<Module>, IOrderedQueryable<Module>>>(),
            It.IsAny<Func<IQueryable<Module>, IQueryable<Module>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Module>().AsQueryable());

        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _updateModuleCommandHandler.Handle(command, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldReturnModuleWithLessons_WhenModuleExists()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);

        _getLessonsByModuleIdQueryHandler = new GetLessonsByModuleIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var moduleId = 1;
        var query = new GetLessonsByModuleIdQuery(moduleId);

        var module = new Module
        {
            ModuleId = moduleId,
            Name = "Math Module",
            Lessons = new List<Lesson>
            {
                new Lesson { LessonId = 1, Name = "Lesson 1" },
                new Lesson { LessonId = 2, Name = "Lesson 2" }
            }
        };

        var moduleList = new List<Module> { module }.AsQueryable();

        _moduleRepoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<Module, bool>>>()))
            .ReturnsAsync(moduleList);

        var mappedResponse = new GetModuleDetailResponse
        {
            ModuleId = module.ModuleId,
            Name = module.Name,
            Lessons = module.Lessons.Select(l => new GetLessonItem
            {
                LessonId = l.LessonId,
                Name = l.Name
            }).ToList()
        };

        _mapperMock.Setup(m => m.Map<GetModuleDetailResponse>(It.IsAny<Module>()))
            .Returns(mappedResponse);

        // Act
        var response = await _getLessonsByModuleIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, response.Code);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual(2, response.Data.Lessons.Count);
        Assert.AreEqual("Lesson 1", response.Data.Lessons[0].Name);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowApiException_WhenModuleNotFound()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);

        _getLessonsByModuleIdQueryHandler = new GetLessonsByModuleIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Arrange
        var query = new GetLessonsByModuleIdQuery(100);

        var emptyModuleList = new List<Module>().AsQueryable(); // using MockQueryable.Moq

        _moduleRepoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<Module, bool>>>(),
            It.IsAny<Func<IQueryable<Module>, IOrderedQueryable<Module>>>(),
            It.IsAny<Func<IQueryable<Module>, IQueryable<Module>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(emptyModuleList);

        // Act + Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _getLessonsByModuleIdQueryHandler.Handle(query, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldReturnModule_WhenModuleExists()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);

        _getModuleByIdQueryHandler = new GetModuleByIdQueryHandler(_unitOfWorkMock.Object);
        // Arrange
        var query = new GetModuleByIdQuery(1);

        var module = new Module
        {
            ModuleId = 1,
            Name = "Math Module",
            Desciption = "Basic math",
            Semester = 1,
            TotalPeriods = 20,
            Curriculum = new Curriculum { Name = "2023 Curriculum" },
            Grade = new Grade { GradeNumber = 5 },
            Book = new Book { BookName = "Math Book" }
        };

        var modules = new List<Module> { module }.AsQueryable();

        _moduleRepoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<Module, bool>>>()))
            .ReturnsAsync(modules);

        // Act
        var response = await _getModuleByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, response.Code);
        Assert.IsNotNull(response.Data);
        Assert.AreEqual("Math Module", response.Data.Name);
        Assert.AreEqual("2023 Curriculum", response.Data.Curriculum);
        Assert.AreEqual("Math Book", response.Data.Book);
        Assert.AreEqual(5, response.Data.GradeNumber);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowApiException_WhenModuleNotFoundGetAllModule()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);

        _getModuleByIdQueryHandler = new GetModuleByIdQueryHandler(_unitOfWorkMock.Object);
        // Arrange
        var query = new GetModuleByIdQuery(999);

        var emptyList = new List<Module>().AsQueryable();

        _moduleRepoMock
            .Setup(r => r.GetAsync(It.IsAny<Expression<Func<Module, bool>>>(),
            It.IsAny<Func<IQueryable<Module>, IOrderedQueryable<Module>>>(),
            It.IsAny<Func<IQueryable<Module>, IQueryable<Module>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(emptyList);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _getModuleByIdQueryHandler.Handle(query, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldReturnPagedModules_WhenDataExists()
    {
        _unitOfWorkMock.Setup(u => u.Modules).Returns(_moduleRepoMock.Object);

        _getModulesQueryHandler = new GetModulesQueryHandler(_unitOfWorkMock.Object);

        // Arrange
        var modules = new List<Module>
        {
            new Module
            {
                ModuleId = 1,
                Name = "Math",
                Desciption = "Basic Math",
                Semester = 1,
                TotalPeriods = 30,
                Curriculum = new Curriculum { Name = "Curriculum A" },
                Grade = new Grade { GradeNumber = 5 },
                Book = new Book { BookName = "Book A" }
            },
            new Module
            {
                ModuleId = 2,
                Name = "Science",
                Desciption = "Basic Science",
                Semester = 2,
                TotalPeriods = 40,
                Curriculum = new Curriculum { Name = "Curriculum B" },
                Grade = new Grade { GradeNumber = 6 },
                Book = new Book { BookName = "Book B" }
            }
        };

        var paginationResult = new BasePaginationEntity<Module>
        {
            Total = 2,
            Data = modules
        };

        _moduleRepoMock
            .Setup(r => r.PaginationAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                  It.IsAny<Expression<Func<Module, bool>>>(),
             It.IsAny<Func<IQueryable<Module>, IIncludableQueryable<Module, object>>>(),
             It.IsAny<Func<IQueryable<Module>, IOrderedQueryable<Module>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(paginationResult);

        var query = new GetModulesQuery
        {
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var response = await _getModulesQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, response.Code);
        Assert.AreEqual(2, response.Data.Items.Count);
        Assert.AreEqual("Math", response.Data.Items[0].Name);
        Assert.AreEqual("Curriculum A", response.Data.Items[0].Curriculum);
        Assert.AreEqual("Book B", response.Data.Items[1].Book);
        Assert.AreEqual(1, response.Data.Page);
        Assert.AreEqual(10, response.Data.PageSize);
        Assert.AreEqual(2, response.Data.TotalSize);
        Assert.AreEqual(1, response.Data.TotalPage); // 2 items, pageSize 10 -> totalPage = 1
    }
    #endregion

    #region Blog
    [TestMethod]
    public async Task Handle_ShouldCreateBlog_WhenRequestIsValid()
    {
        _createBlogCommandHandler = new CreateBlogCommandHandler(_unitOfWorkMock.Object, _dateTimeProviderMock.Object);
        // Arrange
        var request = new CreateBlogCommand
        (
            "Valid Title",
            "Valid body",
            1,
            1,
            19
        );

        var now = DateTime.UtcNow;
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(now);

        _unitOfWorkMock.Setup(x => x.Categories.GetByIdAsync(1)).ReturnsAsync(new Category());
        _unitOfWorkMock.Setup(x => x.TeacherLessons.GetByIdAsync(1)).ReturnsAsync(new TeacherLesson());
        _unitOfWorkMock.Setup(x => x.Blogs.AddAsync(It.IsAny<Blog>())).ReturnsAsync(new Blog());
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _createBlogCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.CREATED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.CREATED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowValidationException_WhenTitleIsEmpty()
    {
        _createBlogCommandHandler = new CreateBlogCommandHandler(_unitOfWorkMock.Object, _dateTimeProviderMock.Object);
        // Arrange
        var request = new CreateBlogCommand
        (
            "",
            "Valid body",
            1,
            1,
            19
        );

        _unitOfWorkMock.Setup(x => x.Categories.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Category());
        _unitOfWorkMock.Setup(x => x.TeacherLessons.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TeacherLesson());

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            _createBlogCommandHandler.Handle(request, CancellationToken.None));

        Assert.IsTrue(ex.Errors.Any(e => e.Contains("Title is required")));
    }

    [TestMethod]
    public async Task Handle_ShouldThrowValidationException_WhenBodyIsEmpty()
    {
        _createBlogCommandHandler = new CreateBlogCommandHandler(_unitOfWorkMock.Object, _dateTimeProviderMock.Object);
        var request = new CreateBlogCommand
        (
            "Valid Title",
            "",
            1,
            1,
            19
        );

        _unitOfWorkMock.Setup(x => x.Categories.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Category());
        _unitOfWorkMock.Setup(x => x.TeacherLessons.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TeacherLesson());

        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            _createBlogCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrowValidationException_WhenCategoryNotFound()
    {
        _createBlogCommandHandler = new CreateBlogCommandHandler(_unitOfWorkMock.Object, _dateTimeProviderMock.Object);
        var request = new CreateBlogCommand
        (
            "Valid Title",
            "Valid Body",
            999,
            1,
            19
        );

        _unitOfWorkMock.Setup(x => x.Categories.GetByIdAsync(999)).ReturnsAsync((Category)null);
        _unitOfWorkMock.Setup(x => x.TeacherLessons.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TeacherLesson());

        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            _createBlogCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrowValidationException_WhenTeacherLessonNotFound()
    {
        _createBlogCommandHandler = new CreateBlogCommandHandler(_unitOfWorkMock.Object, _dateTimeProviderMock.Object);
        var request = new CreateBlogCommand
        (
            "Valid Title",
            "Valid Body",
            1,
            999,
            19
        );

        _unitOfWorkMock.Setup(x => x.Categories.GetByIdAsync(1)).ReturnsAsync(new Category());
        _unitOfWorkMock.Setup(x => x.TeacherLessons.GetByIdAsync(999)).ReturnsAsync((TeacherLesson)null);

        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            _createBlogCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldDisableBlog_WhenBlogIsActive()
    {
        _disableBlogCommandHandler = new DisableBlogCommandHandler(_unitOfWorkMock.Object);// Arrange
        var blog = new Blog { BlogId = 1, IsActive = true };
        var queryableBlog = new List<Blog> { blog }.AsQueryable();

        _unitOfWorkMock.Setup(x => x.Blogs.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>(),
            It.IsAny<Func<IQueryable<Blog>, IOrderedQueryable<Blog>>>(),
            It.IsAny<Func<IQueryable<Blog>, IQueryable<Blog>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(queryableBlog);

        _unitOfWorkMock.Setup(x => x.Blogs.UpdateAsync(It.IsAny<Blog>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        var request = new DisableBlogCommand(1);

        // Act
        var result = await _disableBlogCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual(false, blog.IsActive);
        Assert.AreEqual((int)ResponseCode.DISABLED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.DISABLED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_ShouldEnableBlog_WhenBlogIsInactive()
    {
        _disableBlogCommandHandler = new DisableBlogCommandHandler(_unitOfWorkMock.Object);// Arrange
        var blog = new Blog { BlogId = 2, IsActive = false };
        var queryableBlog = new List<Blog> { blog }.AsQueryable();

        _unitOfWorkMock.Setup(x => x.Blogs.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>(),
            It.IsAny<Func<IQueryable<Blog>, IOrderedQueryable<Blog>>>(),
            It.IsAny<Func<IQueryable<Blog>, IQueryable<Blog>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(queryableBlog);

        _unitOfWorkMock.Setup(x => x.Blogs.UpdateAsync(It.IsAny<Blog>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        var request = new DisableBlogCommand(2);

        // Act
        var result = await _disableBlogCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual(true, blog.IsActive);
        Assert.AreEqual((int)ResponseCode.DISABLED_SUCCESS, result.Code);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowException_WhenBlogNotFound()
    {
        _disableBlogCommandHandler = new DisableBlogCommandHandler(_unitOfWorkMock.Object);// Arrange
        _unitOfWorkMock.Setup(x => x.Blogs.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>(),
            It.IsAny<Func<IQueryable<Blog>, IOrderedQueryable<Blog>>>(),
            It.IsAny<Func<IQueryable<Blog>, IQueryable<Blog>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Blog>().AsQueryable());

        var request = new DisableBlogCommand(99);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _disableBlogCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldUpdateBlog_WhenValidRequest()
    {
        _updateBlogCommandHandler = new UpdateBlogCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        var blog = new Blog { BlogId = 1, Title = "Old Title", Body = "Old Body" };
        var request = new UpdateBlogCommand
        (1,
            new UpdateBlogRequest
            (
                "New Title",
                "New Body"
            )
        );

        _unitOfWorkMock.Setup(u => u.Blogs.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>(),
            It.IsAny<Func<IQueryable<Blog>, IOrderedQueryable<Blog>>>(),
            It.IsAny<Func<IQueryable<Blog>, IQueryable<Blog>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Blog> { blog }.AsQueryable());

        _unitOfWorkMock.Setup(u => u.Blogs.UpdateAsync(It.IsAny<Blog>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _updateBlogCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual("New Title", blog.Title);
        Assert.AreEqual("New Body", blog.Body);
        Assert.AreEqual((int)ResponseCode.UPDATED_SUCCESS, result.Code);
        Assert.AreEqual(ResponseCode.UPDATED_SUCCESS.GetDescription(), result.Message);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowValidationException_WhenBlogNotFound()
    {
        _updateBlogCommandHandler = new UpdateBlogCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        _unitOfWorkMock.Setup(u => u.Blogs.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>(),
            It.IsAny<Func<IQueryable<Blog>, IOrderedQueryable<Blog>>>(),
            It.IsAny<Func<IQueryable<Blog>, IQueryable<Blog>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Blog>().AsQueryable());

        var request = new UpdateBlogCommand
        (999,
            new UpdateBlogRequest
            (
                "Test",
                "Test"
            )
        );

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            _updateBlogCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldThrowValidationException_WhenTitleOrBodyMissing()
    {
        _updateBlogCommandHandler = new UpdateBlogCommandHandler(_unitOfWorkMock.Object);
        // Arrange
        var blog = new Blog { BlogId = 1, Title = "Old", Body = "Old" };

        _unitOfWorkMock.Setup(u => u.Blogs.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>(),
            It.IsAny<Func<IQueryable<Blog>, IOrderedQueryable<Blog>>>(),
            It.IsAny<Func<IQueryable<Blog>, IQueryable<Blog>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Blog> { blog }.AsQueryable());

        var request = new UpdateBlogCommand
        (
             1,
            new UpdateBlogRequest
            (
                 "",
                 ""
            )
        );

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() =>
            _updateBlogCommandHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldReturnBlogDetail_WhenBlogExists()
    {
        _getBlogByIdQueryHandler = new GetBlogByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);// Arrange
        var blogId = 1;

        var mockBlog = new Blog
        {
            BlogId = blogId,
            Title = "Sample Blog",
            Category = new Category { CategoryId = 1, CategoryName = "Tech" },
            Comments = new List<Comment>
            {
                new Comment { User = new User { UserId = 1, Fullname = "User A" }, CommentBody = "Great post!" }
            }
        };

        var mockBlogList = new List<Blog> { mockBlog }.AsQueryable();

        _unitOfWorkMock.Setup(u => u.Blogs.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>()))
            .ReturnsAsync(mockBlogList);

        _mapperMock.Setup(m => m.Map<GetBlogDetailResponse>(It.IsAny<Blog>()))
            .Returns(new GetBlogDetailResponse { BlogId = blogId, Title = "Sample Blog" });

        var request = new GetBlogByIdQuery(blogId);

        // Act
        var result = await _getBlogByIdQueryHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
    }

    [TestMethod]
    public async Task Handle_ShouldThrowApiException_WhenBlogNotFound()
    {
        _getBlogByIdQueryHandler = new GetBlogByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);// Arrange
        _unitOfWorkMock.Setup(u => u.Blogs.GetAsync(It.IsAny<Expression<Func<Blog, bool>>>(),
            It.IsAny<Func<IQueryable<Blog>, IOrderedQueryable<Blog>>>(),
            It.IsAny<Func<IQueryable<Blog>, IQueryable<Blog>>>(),
            It.IsAny<bool>()))
            .ReturnsAsync(new List<Blog>().AsQueryable());

        var request = new GetBlogByIdQuery(999);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ApiException>(() =>
            _getBlogByIdQueryHandler.Handle(request, CancellationToken.None));
    }

    [TestMethod]
    public async Task Handle_ShouldReturnPaginatedBlogList_WhenCalled()
    {
        _getBlogsQueryHandler = new GetBlogsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        // Arrange
        var blogs = new List<Blog>
        {
            new Blog { BlogId = 1, Title = "Blog 1" },
            new Blog { BlogId = 2, Title = "Blog 2" }
        };

        var paginatedBlogData = new PaginatedList<Blog>(blogs, totalRecords: 2, currentPage: 1, pageSize: 10);

        _unitOfWorkMock.Setup(u => u.Blogs.PaginatedListAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>(),
                It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(paginatedBlogData);

        var mappedBlogData = new PaginatedList<GetBlogResponse>(
            new List<GetBlogResponse>
            {
                new GetBlogResponse { BlogId = 1, Title = "Blog 1" },
                new GetBlogResponse { BlogId = 2, Title = "Blog 2" }
            },
            totalRecords: 2,
            currentPage: 1,
            pageSize: 10);

        _mapperMock.Setup(m => m.Map<PaginatedList<GetBlogResponse>>(paginatedBlogData))
            .Returns(mappedBlogData);

        var query = new GetBlogsQuery
        (
            "",
            "Title",
            "asc",
            false,
            19,
            null,
            1,
            10
        );

        // Act
        var result = await _getBlogsQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(2, result.Data.Items.Count);
        Assert.AreEqual("Blog 1", result.Data.Items[0].Title);
    }
    #endregion

    #region Categories
    [TestMethod]
    public async Task Handle_ShouldReturnCategoryList_WhenCategoriesExist()
    {
        _getCategoriesQueryHandler = new GetCategoriesQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        // Arrange
        var categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Math" },
            new Category { CategoryId = 2, CategoryName = "Science" }
        };

        _unitOfWorkMock.Setup(u => u.Categories.GetAllAsync(
            It.IsAny<Expression<Func<Category, bool>>>(),
            It.IsAny<Func<IQueryable<Category>, IQueryable<Category>>>(),
            It.IsAny<bool>()
        )).ReturnsAsync(categories.AsQueryable());

        var expectedMappedCategories = new List<GetCategoryResponse>
        {
            new GetCategoryResponse (1, "Math" ),
            new GetCategoryResponse ( 2, "Science" )
        };

        _mapperMock.Setup(m => m.Map<List<GetCategoryResponse>>(It.IsAny<List<Category>>()))
            .Returns(expectedMappedCategories);

        var query = new GetCategoriesQuery();

        // Act
        var result = await _getCategoriesQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(2, result.Data.Count);
    }
    #endregion

    #region City
    [TestMethod]
    public async Task Handle_ShouldReturnCityWithDistricts_WhenCityExists()
    {
        _getDistrictsByCityIdQueryHandler = new GetDistrictsByCityIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        // Arrange
        var cityId = 1;
        var city = new City
        {
            CityId = cityId,
            CityName = "Ho Chi Minh",
            Districts = new List<District>
            {
                new District { DistrictId = 1, DistrictName = "District 1" },
                new District { DistrictId = 2, DistrictName = "District 2" }
            }
        };

        var cityList = new List<City> { city }.AsQueryable();

        _unitOfWorkMock.Setup(u => u.Cities.GetAsync(
           It.IsAny<Expression<Func<City, bool>>>(),
            It.IsAny<Func<IQueryable<City>, IOrderedQueryable<City>>>(),
            It.IsAny<Func<IQueryable<City>, IQueryable<City>>>(),
            It.IsAny<bool>()
        )).ReturnsAsync(cityList);

        var expectedResponse = new GetCityDetailResponse
        (
            city.CityId,
            city.CityName,
            new List<GetDistrictResponse>
            {
                new GetDistrictResponse (1,"District 1"),
                new GetDistrictResponse ( 2,"District 2" )
            }
        );

        _mapperMock.Setup(m => m.Map<GetCityDetailResponse>(It.IsAny<City>()))
            .Returns(expectedResponse);

        var request = new GetDistrictsByCityIdQuery(cityId);

        // Act
        var result = await _getDistrictsByCityIdQueryHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(2, result.Data.Districts.Count);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnListOfCities_WhenCitiesExist()
    {
        _getCitiesQueryHandler = new GetCitiesQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        // Arrange
        var cities = new List<City>
        {
            new City { CityId = 1, CityName = "Hanoi" },
            new City { CityId = 2, CityName = "Ho Chi Minh" }
        };

        _unitOfWorkMock.Setup(u => u.Cities.GetAllAsync(
            It.IsAny<Expression<Func<City, bool>>>(),
            It.IsAny<Func<IQueryable<City>, IQueryable<City>>>(),
            It.IsAny<bool>()
        ))
            .ReturnsAsync(cities.AsQueryable());

        var expectedResponses = new List<GetCityResponse>
        {
            new GetCityResponse ( 1, "Hanoi" ),
            new GetCityResponse ( 2, "Ho Chi Minh" )
        };

        _mapperMock.Setup(m => m.Map<List<GetCityResponse>>(It.IsAny<List<City>>()))
            .Returns(expectedResponses);

        var request = new GetCitiesQuery();

        // Act
        var result = await _getCitiesQueryHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.AreEqual(2, result.Data.Count);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnEmptyList_WhenNoCitiesExist()
    {
        _getCitiesQueryHandler = new GetCitiesQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        // Arrange
        var cities = new List<City>();

        _unitOfWorkMock.Setup(u => u.Cities.GetAllAsync(
            It.IsAny<Expression<Func<City, bool>>>(),
            It.IsAny<Func<IQueryable<City>, IQueryable<City>>>(),
            It.IsAny<bool>()
        ))
            .ReturnsAsync(cities.AsQueryable());

        _mapperMock.Setup(m => m.Map<List<GetCityResponse>>(It.IsAny<List<City>>()))
            .Returns(new List<GetCityResponse>());

        var request = new GetCitiesQuery();

        // Act
        var result = await _getCitiesQueryHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.AreEqual((int)ResponseCode.SUCCESS, result.Code);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(0, result.Data.Count);
    }
    #endregion
}
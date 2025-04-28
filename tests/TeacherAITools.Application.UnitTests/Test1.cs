using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Moq;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.Common.Models.Requests;
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
using TeacherAITools.Domain.Wrappers;
using TeacherAITools.Infrastructure.Modules;

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
    private Mock<ICurrentUserService> _currentUserServiceMock;

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

        _userRepositoryMock.Setup(r => r.CheckSchoolManagerAsync(command.RoleId, command.GradeId, command.SchoolId))
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

        _userRepositoryMock.Setup(r => r.CheckSchoolManagerAsync(command.RoleId, command.GradeId, command.SchoolId))
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

        _userRepositoryMock.Setup(r => r.CheckSchoolManagerAsync(2, 1, 1)).ReturnsAsync(0);
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

        _userRepositoryMock.Setup(r => r.CheckSchoolManagerAsync(2, 1, 1)).ReturnsAsync(1); // Manager exists

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

        _userRepositoryMock.Setup(r => r.CheckSchoolManagerAsync(2, 1, 1)).ReturnsAsync(0); // Vice manager exists

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
        var fakeUser = new User { Email = "fail@example.com" };

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
            "Male",
            1
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
            "address",
            1)
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
            "fullname",
            "email",
            "phonenumber",
            DateOnly.MinValue,
            Gender.Male,
            "address",
            1
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

        var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Fullname", "Fullname is required")
            };

        var a = new ValidationResult(validationFailures);

        var validatorMock = new Mock<FluentValidation.IValidator<UpdateUserRequest>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<UpdateUserRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(a);

        // Act & Assert
        var ex = await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            await _updateUserCommandHandler.Handle(command, CancellationToken.None));

        Assert.IsTrue(ex.Errors.Contains("Fullname is required"));
    }

    [TestMethod]
    public async Task Handle_ShouldUploadProfileImageSuccessfully()
    {
        // Arrange
        var command = new UploadProfileImgCommand ( MockFile("test.jpg", 1024 * 1024) );
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
        var command = new UploadProfileImgCommand (MockFile("file.txt", 1024) );
        var handler = new UploadProfileImgCommandHandler(_unitOfWorkMock.Object, _uploadFileServiceMock.Object, _currentUserServiceMock.Object);

        await handler.Handle(command, CancellationToken.None);
    }

    [TestMethod]
    [ExpectedException(typeof(ApiException))]
    public async Task Handle_ShouldThrowException_WhenFileTooLarge()
    {
        var command = new UploadProfileImgCommand (MockFile("file.jpg", 6 * 1024 * 1024) );
        var handler = new UploadProfileImgCommandHandler(_unitOfWorkMock.Object, _uploadFileServiceMock.Object, _currentUserServiceMock.Object);

        await handler.Handle(command, CancellationToken.None);
    }

    [TestMethod]
    [ExpectedException(typeof(ApiException))]
    public async Task Handle_ShouldThrowException_WhenCurrentUserNull()
    {
        var command = new UploadProfileImgCommand (MockFile("test.jpg", 1024 * 1024) );
        _currentUserServiceMock.Setup(x => x.CurrentPrincipal).Returns((string)null);

        var handler = new UploadProfileImgCommandHandler(_unitOfWorkMock.Object, _uploadFileServiceMock.Object, _currentUserServiceMock.Object);

        await handler.Handle(command, CancellationToken.None);
    }

    [TestMethod]
    [ExpectedException(typeof(ApiException))]
    public async Task Handle_ShouldThrowException_WhenUserNotFound()
    {
        var command = new UploadProfileImgCommand (MockFile("test.jpg", 1024 * 1024) );
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
}

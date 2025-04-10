using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Users.Commands.ChangePassword;
using TeacherAITools.Application.Users.Commands.CreateUser;
using TeacherAITools.Application.Users.Commands.DisableUser;
using TeacherAITools.Application.Users.Commands.UpdateUser;
using TeacherAITools.Application.Users.Commands.UploadProfileImg;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Application.Users.Queries.GetTeacherLessonsById;
using TeacherAITools.Application.Users.Queries.GetUserById;
using TeacherAITools.Application.Users.Queries.GetUsers;
using TeacherAITools.Application.Users.Queries.GetUserUpdateById;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion(1)]
    public class UsersController(
        IMediator mediator,
        ILogger<UsersController> logger) : ApiController(mediator, logger)
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] CreateUserCommand request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (ValidationException e)
            {
                return BadRequest(new
                {
                    errorCode = e.ErrorCode,
                    errors = e.Errors,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await mediator.Send(new GetUserByIdQuery(id)));
            }
            catch (ApiException e)
            {
                return NotFound(new
                {
                    errorCode = e.ErrorCode,
                    error = e.Error,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpGet("{id}/update")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserUpdateResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserUpdateByIdAsync(int id)
        {
            try
            {
                return Ok(await mediator.Send(new GetUserUpdateByIdQuery(id)));
            }
            catch (ApiException e)
            {
                return NotFound(new
                {
                    errorCode = e.ErrorCode,
                    error = e.Error,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpGet("{id}/lessons")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserLessonsResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTeacherLessonsByIdAsync(int id)
        {
            try
            {
                return Ok(await mediator.Send(new GetTeacherLessonsByIdQuery(id)));
            }
            catch (ApiException e)
            {
                return NotFound(new
                {
                    errorCode = e.ErrorCode,
                    error = e.Error,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<PaginatedList<GetUserResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetUsersQuery query)
        {
            try
            {
                return Ok(await mediator.Send(query));
            }
            catch (ApiException e)
            {
                return BadRequest(new
                {
                    errorCode = e.ErrorCode,
                    error = e.Error,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                return Ok(await mediator.Send(new UpdateUserCommand(id, request)));
            }
            catch (ValidationException e)
            {
                return NotFound(new
                {
                    errorCode = e.ErrorCode,
                    errors = e.Errors,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] ChangePasswordCommand request)
        {
            try
            {
                return Ok(await mediator.Send(request));
            }
            catch (ValidationException e)
            {
                return NotFound(new
                {
                    errorCode = e.ErrorCode,
                    errors = e.Errors,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                return Ok(await mediator.Send(new DisableUserCommand(id)));
            }
            catch (ApiException e)
            {
                return NotFound(new
                {
                    errorCode = e.ErrorCode,
                    error = e.Error,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpPost("profile-img")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            try
            {
                return Ok(await mediator.Send(new UploadProfileImgCommand(file)));
            }
            catch (ApiException e)
            {
                return BadRequest(new
                {
                    errorCode = e.ErrorCode,
                    error = e.Error,
                    errorMessage = e.ErrorMessage
                });
            }
        }
    }
}

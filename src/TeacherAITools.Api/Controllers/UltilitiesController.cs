using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Ultilities.Commands.CreateApply;
using TeacherAITools.Application.Ultilities.Commands.CreateKnowLedge;
using TeacherAITools.Application.Ultilities.Commands.CreatePractice;
using TeacherAITools.Application.Ultilities.Commands.CreateStartUp;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/categories")]
    [ApiVersion(1)]
    public class UltilitiesController(
        IMediator mediator,
        ILogger<UltilitiesController> logger) : ApiController(mediator, logger)
    {
        [HttpPost("start-ups")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAsync(string goal, string teacherActivities, string studentActivities, int lessonId)
        {
            try
            {
                return Ok(await mediator.Send(new CreateStartUpCommand(goal, teacherActivities, studentActivities, lessonId)));
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

        [HttpPost("knowledge")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoAsync(string goal, string teacherActivities, string studentActivities, int lessonId)
        {
            try
            {
                return Ok(await mediator.Send(new CreateKnowLedgeCommand(goal, teacherActivities, studentActivities, lessonId)));
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

        [HttpPost("practices")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoinAsync(string goal, string teacherActivities, string studentActivities, int lessonId)
        {
            try
            {
                return Ok(await mediator.Send(new CreatePracticeCommand(goal, teacherActivities, studentActivities, lessonId)));
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

        [HttpPost("applies")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LginAsync(string goal, string teacherActivities, string studentActivities, int lessonId)
        {
            try
            {
                return Ok(await mediator.Send(new CreateApplyCommand(goal, teacherActivities, studentActivities, lessonId)));
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
    }
}

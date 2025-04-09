using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.TeacherLessons.Commands.CreateTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessonById;
using TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessons;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/teacher-lessons")]
    [ApiVersion(1)]
    public class TeacherLessonsController(
        IMediator mediator,
        ILogger<TeacherLessonsController> logger) : ApiController(mediator, logger)
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] CreateTeacherLessonCommand request)
        {
            try
            {
                return Ok(await mediator.Send(request));
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

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<PaginatedList<GetTeacherLessonResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetTeacherLessonsQuery query)
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await mediator.Send(new GetTeacherLessonByIdQuery(id)));
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
    }
}

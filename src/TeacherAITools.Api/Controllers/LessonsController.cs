using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Lessons.Commands.CreateLesson;
using TeacherAITools.Application.Lessons.Commands.DeleteLesson;
using TeacherAITools.Application.Lessons.Commands.UpdateLesson;
using TeacherAITools.Application.Lessons.Common;
using TeacherAITools.Application.Lessons.Queries.GetLessonById;
using TeacherAITools.Application.Lessons.Queries.GetLessonInfoById;
using TeacherAITools.Application.Lessons.Queries.GetLessons;
using TeacherAITools.Application.Lessons.Queries.GetPromptById;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/lessons")]
    [ApiVersion(1)]
    public class LessonsController(IMediator mediator, ILogger<LessonsController> logger) : ApiController(mediator, logger)
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<LessonsController> _logger = logger;

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateLessonRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateLessonCommand(request)));
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
        [ProducesResponseType(typeof(Response<GetLessonDetailResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetLessonByIdQuery(id)));
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

        [HttpGet("{id}/prompt")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetPromptResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPromptByIdAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetPromptByIdQuery(id)));
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

        [HttpGet("{id}/info")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetPromptResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLessonInfoById(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetLessonInfoByIdQuery(id)));
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
        [ProducesResponseType(typeof(Response<List<GetLessonResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetLessonsQuery query)
        {
            try
            {
                return Ok(await _mediator.Send(query));
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
        [ProducesResponseType(typeof(Response<GetLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateLessonRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateLessonCommand(id, request)));
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

        //[HttpPut("{id}/info")]
        //[AllowAnonymous]
        //[ProducesResponseType(typeof(Response<GetLessonResponse>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateLessonInfoRequest request)
        //{
        //    try
        //    {
        //        return Ok(await mediator.Send(new UpdateLessonInfoCommand(id, request)));
        //    }
        //    catch (ValidationException e)
        //    {
        //        return NotFound(new
        //        {
        //            errorCode = e.ErrorCode,
        //            errors = e.Errors,
        //            errorMessage = e.ErrorMessage
        //        });
        //    }
        //}

        [HttpDelete("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteLessonCommand(id)));
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

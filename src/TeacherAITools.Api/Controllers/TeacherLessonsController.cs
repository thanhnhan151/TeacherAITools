using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.TeacherLessons.Commands.CreatePendingTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Commands.CreateTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Commands.UpdateStatusTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Commands.UpdateTeacherLesson;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessonById;
using TeacherAITools.Application.TeacherLessons.Queries.GetTeacherLessons;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/lesson-plans")]
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

        [HttpPost("pending")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePendingAsync([FromBody] CreatePendingTeacherLessonCommand request)
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

        [HttpPut("{id}/draft")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DraftTeacherLessonAsync(int id)
        {
            try
            {
                var request = new UpdateStatusTeacherLessonRequest { Status = Domain.Common.LessonStatus.Draft };
                return Ok(await mediator.Send(new UpdateStatusTeacherLessonCommand(id, request)));
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

        //[HttpPut("{id}/cancel")]
        //[AllowAnonymous]
        //[ProducesResponseType(typeof(Response<GetDetailTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> CancelTeacherLessonAsync(int id)
        //{
        //    try
        //    {
        //        var request = new UpdateStatusTeacherLessonRequest { Status = Domain.Common.LessonStatus.Cancelled };
        //        return Ok(await mediator.Send(new UpdateStatusTeacherLessonCommand(id, request)));
        //    }
        //    catch (ApiException e)
        //    {
        //        return NotFound(new
        //        {
        //            errorCode = e.ErrorCode,
        //            error = e.Error,
        //            errorMessage = e.ErrorMessage
        //        });
        //    }
        //}

        [HttpPut("{id}/pending")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PendingTeacherLessonAsync(int id)
        {
            try
            {
                var request = new UpdateStatusTeacherLessonRequest { Status = Domain.Common.LessonStatus.Pending };
                return Ok(await mediator.Send(new UpdateStatusTeacherLessonCommand(id, request)));
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

        [HttpPut("{id}/reject")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RejectTeacherLessonAsync(int id, [FromBody] string disapprovedReason)
        {
            try
            {
                var request = new UpdateStatusTeacherLessonRequest { Status = Domain.Common.LessonStatus.Rejected, DisapprovedReason = disapprovedReason };
                return Ok(await mediator.Send(new UpdateStatusTeacherLessonCommand(id, request)));
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

        [HttpPut("{id}/approve")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ApproveTeacherLessonAsync(int id)
        {
            try
            {
                var request = new UpdateStatusTeacherLessonRequest { Status = Domain.Common.LessonStatus.Approved };
                return Ok(await mediator.Send(new UpdateStatusTeacherLessonCommand(id, request)));
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

        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailTeacherLessonResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateTeacherLessonAsync(int id, [FromBody] UpdateTeacherLessonRequest request)
        {
            try
            {
                return Ok(await mediator.Send(new UpdateTeacherLessonCommand(id, request)));
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

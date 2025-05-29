using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Curriculums.Commands.CreateCurriculum;
using TeacherAITools.Application.Curriculums.Commands.CreateCurriculumDetail;
using TeacherAITools.Application.Curriculums.Commands.CreateFeedbackByCurriculumId;
using TeacherAITools.Application.Curriculums.Commands.DeleteCurriculum;
using TeacherAITools.Application.Curriculums.Commands.DeleteCurriculumDetail;
using TeacherAITools.Application.Curriculums.Commands.UpdateCurriculum;
using TeacherAITools.Application.Curriculums.Commands.UpdateCurriculumDetail;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Application.Curriculums.Queries.GetCurriculumById;
using TeacherAITools.Application.Curriculums.Queries.GetCurriculums;
using TeacherAITools.Application.Curriculums.Queries.GetCurriculumSubSections;
using TeacherAITools.Application.Curriculums.Queries.GetFeedbacksByCurriculumId;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/curriculums")]
    [ApiVersion(1)]
    public class CurriculumsController(
        IMediator mediator,
        ILogger<CurriculumsController> logger) : ApiController(mediator, logger)
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<CurriculumsController> _logger = logger;

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetCurriculumResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCurriculumRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateCurriculumCommand(request)));
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

        [HttpPost("{id}/feedbacks")]
        [Authorize]
        [ProducesResponseType(typeof(Response<GetCurriculumResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync(int id, [FromBody] CreateFeedbackRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateFeedbackByCurriculumIdCommand(id, request)));
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

        [HttpPost("detail")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailCurriculumResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCurriculumDetailAsync([FromBody] CreateCurriculumDetailRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateCurriculumDetailCommand(request)));
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
        [ProducesResponseType(typeof(Response<GetCurriculumResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetCurriculumByIdQuery(id)));
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

        [HttpGet("sub-sections")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<List<GetCurriculumSubSectionsResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return Ok(await _mediator.Send(new GetCurriculumSubSectionsQuery()));
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

        [HttpGet("{id}/feedbacks")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<List<GetCurriculumFeedbackResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetFeedbacksByCurriculumIdAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetFeedbacksByCurriculumIdQuery(id)));
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
        [ProducesResponseType(typeof(Response<List<GetCurriculumResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetCurriculumsQuery query)
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
        [ProducesResponseType(typeof(Response<GetCurriculumResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCurriculumRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateCurriculumCommand(id, request)));
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

        [HttpPut("detail/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailCurriculumResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCurriculumDetailAsync(int id, [FromBody] UpdateCurriculumDetailRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateCurriculumDetailCommand(id, request)));
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

        [HttpDelete("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetCurriculumResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteCurriculumCommand(id)));
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

        [HttpDelete("detail/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDetailCurriculumResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteCurriculumDetailAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteCurriculumDetailCommand(id)));
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

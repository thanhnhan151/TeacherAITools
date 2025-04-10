using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Prompts.Commands.CreatePrompt;
using TeacherAITools.Application.Prompts.Commands.UpdatePrompt;
using TeacherAITools.Application.Prompts.Commnon;
using TeacherAITools.Application.Prompts.Queries.GetPrompts;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/prompts")]
    [ApiVersion(1)]
    public class PromptsController(
        IMediator mediator,
        ILogger<PromptsController> logger) : ApiController(mediator, logger)
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] CreatePromptCommand request)
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
                    errors = e.Error,
                    errorMessage = e.ErrorMessage
                });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<PaginatedList<GetPromptResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetPromptsQuery query)
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
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] string description)
        {
            try
            {
                return Ok(await mediator.Send(new UpdatePromptCommand(id, description)));
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

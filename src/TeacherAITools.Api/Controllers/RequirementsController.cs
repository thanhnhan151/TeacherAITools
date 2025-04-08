using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Requirements.Common;
using TeacherAITools.Application.Requirements.Queries.GetRequirements;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/requirements")]
    [ApiVersion(1)]
    public class RequirementsController(
        IMediator mediator,
        ILogger<RequirementsController> logger) : ApiController(mediator, logger)
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<List<GetRequirementResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await mediator.Send(new GetRequirementsQuery()));
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

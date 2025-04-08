using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.SchoolSupplies.Common;
using TeacherAITools.Application.SchoolSupplies.Queries.GetSchoolSupplies;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/school-supplies")]
    [ApiVersion(1)]
    public class SchoolSuppliesController(
        IMediator mediator,
        ILogger<SchoolSuppliesController> logger) : ApiController(mediator, logger)
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<List<GetSchoolSupplyResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await mediator.Send(new GetSchoolSuppliesQuery()));
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

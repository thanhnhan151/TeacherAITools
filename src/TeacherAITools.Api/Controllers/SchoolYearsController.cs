using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.SchoolYears.Common;
using TeacherAITools.Application.SchoolYears.Queries.GetSchoolYears;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/school-years")]
    [ApiVersion(1)]
    public class SchoolYearsController(
        IMediator mediator,
        ILogger<SchoolYearsController> logger) : ApiController(mediator, logger)
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<List<GetSchoolYearResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await mediator.Send(new GetSchoolYearsQuery()));
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

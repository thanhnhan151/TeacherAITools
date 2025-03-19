using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Districts.Common;
using TeacherAITools.Application.Districts.Queries.GetWardsByDistrictId;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/districts")]
    [ApiVersion(1)]
    public class DistrictsController(
        IMediator mediator,
        ILogger<DistrictsController> logger) : ApiController(mediator, logger)
    {
        [HttpGet("{id}/wards")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetDistrictDetailResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await mediator.Send(new GetWardsByDistrictIdQuery(id)));
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

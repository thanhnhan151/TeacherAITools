using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion(1)]
    public class AuthenticationController(ISender _mediator) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] )
        {

        }
    }
}

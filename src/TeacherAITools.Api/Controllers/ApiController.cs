using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly IMediator mediator;
        private readonly ILogger logger;

        protected ApiController(
            IMediator mediator,
            ILogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public IActionResult Ok<T>(Response<T> result)
        {
            string? message = result?.Message;
            Dictionary<string, string?>? errors = [];

            return StatusCode((int)HttpStatusCode.OK, new Response<T>()
            {
                Data = result.Data,
                Code = result.Code,
                Message = message
            });
        }
    }
}

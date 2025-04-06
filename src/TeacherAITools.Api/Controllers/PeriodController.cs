using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Periods.Commands.CreatePeriod;
using TeacherAITools.Application.Periods.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/periods")]
    [ApiVersion(1)]
    public class PeriodController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PeriodController> _logger;

        public PeriodController(IMediator mediator, ILogger<PeriodController> logger) : base(mediator, logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetPeriodResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePeriodRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new CreatePeriodCommand(request)));
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
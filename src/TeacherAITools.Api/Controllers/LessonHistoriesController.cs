using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.LessonHistories.Commands.CreateLessonHistory;
using TeacherAITools.Application.LessonHistories.Common;
using TeacherAITools.Application.LessonHistories.Queries.GetLessonHistories;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/lesson-histories")]
    [ApiVersion(1)]
    public class LessonHistoriesController(IMediator mediator, ILogger<LessonsController> logger) : ApiController(mediator, logger)
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<LessonsController> _logger = logger;

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetLessonHistoryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateLessonHistoryRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateLessonHistoryCommand(request)));
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
        [ProducesResponseType(typeof(Response<List<GetLessonHistoryResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetLessonHistoriesQuery query)
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
    }
}
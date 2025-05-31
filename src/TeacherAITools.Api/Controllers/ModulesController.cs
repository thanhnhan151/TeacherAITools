using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Modules.Commands.CreateModule;
using TeacherAITools.Application.Modules.Commands.DeleteModule;
using TeacherAITools.Application.Modules.Common;
using TeacherAITools.Application.Modules.Queries.GetLessonsByModuleId;
using TeacherAITools.Application.Modules.Queries.GetModuleById;
using TeacherAITools.Application.Modules.Queries.GetModules;
using TeacherAITools.Application.Users.Commands.UpdateUser;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/modules")]
    [ApiVersion(1)]
    public class ModulesController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ModulesController> _logger;

        public ModulesController(IMediator mediator, ILogger<ModulesController> logger) : base(mediator, logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetModuleResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateModuleRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new CreateModuleCommand(request)));
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
        [ProducesResponseType(typeof(Response<GetModuleResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetModuleByIdQuery(id)));
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
        [ProducesResponseType(typeof(Response<List<GetModuleResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetModulesQuery query)
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

        [HttpGet("{id}/lessons")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<List<GetModuleResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync(int id, int? roleId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetLessonsByModuleIdQuery(id, roleId)));
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
        [ProducesResponseType(typeof(Response<GetModuleResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateModuleRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateModuleCommand(id, request)));
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
        [ProducesResponseType(typeof(Response<GetModuleResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteModuleCommand(id)));
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

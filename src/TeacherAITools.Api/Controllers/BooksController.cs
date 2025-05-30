using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Books.Common;
using TeacherAITools.Application.Categories.Queries.GetCategories;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/books")]
    [ApiVersion(1)]
    public class BooksController(
        IMediator mediator,
        ILogger<BooksController> logger) : ApiController(mediator, logger)
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<List<GetBookResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await mediator.Send(new GetCategoriesQuery()));
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

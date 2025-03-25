using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeacherAITools.Application.Blogs.Commands.CreateBlog;
using TeacherAITools.Application.Blogs.Commands.DisableBlog;
using TeacherAITools.Application.Blogs.Commands.UpdateBlog;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Application.Blogs.Queries.GetBlogById;
using TeacherAITools.Application.Blogs.Queries.GetBlogs;
using TeacherAITools.Application.Comments.Commands.CreateComment;
using TeacherAITools.Application.Comments.Commands.DisableComment;
using TeacherAITools.Application.Comments.Commands.UpdateComment;
using TeacherAITools.Application.Comments.Common;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Users.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Api.Controllers
{
    [Route("api/v{version:apiVersion}/blogs")]
    [ApiVersion(1)]
    public class BlogsController(
        IMediator mediator,
        ILogger<BlogsController> logger) : ApiController(mediator, logger)
    {
        #region Blog
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetBlogResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] CreateBlogCommand request)
        {
            try
            {
                return Ok(await mediator.Send(request));
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
        [ProducesResponseType(typeof(Response<PaginatedList<GetBlogResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetBlogsQuery query)
        {
            try
            {
                return Ok(await mediator.Send(query));
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

        [HttpGet("{blogId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetBlogDetailResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync(int blogId)
        {
            try
            {
                return Ok(await mediator.Send(new GetBlogByIdQuery(blogId)));
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

        [HttpPut("{blogId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetBlogResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(int blogId, [FromBody] UpdateBlogRequest request)
        {
            try
            {
                return Ok(await mediator.Send(new UpdateBlogCommand(blogId, request)));
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

        [HttpDelete("{blogId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetBlogResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync(int blogId)
        {
            try
            {
                return Ok(await mediator.Send(new DisableBlogCommand(blogId)));
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
        #endregion

        #region Comment
        [HttpPost("{blogId}/comments")]
        [Authorize]
        [ProducesResponseType(typeof(Response<GetUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddCommentAsync(int blogId, [FromBody] CreateUpdateCommentRequest request)
        {
            try
            {
                return Ok(await mediator.Send(new CreateCommentCommand(blogId, request)));
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

        [HttpPut("{blogId}/comments/{commentId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCommentAsync(int blogId, int commentId, [FromBody] CreateUpdateCommentRequest request)
        {
            try
            {
                return Ok(await mediator.Send(new UpdateCommentCommand(blogId, commentId, request)));
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

        [HttpDelete("{blogId}/comments/{commentId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<GetUserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteCommentAsync(int blogId, int commentId)
        {
            try
            {
                return Ok(await mediator.Send(new DisableCommentCommand(blogId, commentId)));
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
        #endregion
    }
}

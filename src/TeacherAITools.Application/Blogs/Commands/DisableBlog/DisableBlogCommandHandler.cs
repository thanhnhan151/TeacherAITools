using MediatR;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Commands.DisableBlog
{
    public class DisableBlogCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<DisableBlogCommand, Response<GetBlogResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetBlogResponse>> Handle(DisableBlogCommand request, CancellationToken cancellationToken)
        {
            var blogQuery = await _unitOfWork.Blogs.GetAsync(expression: u => u.BlogId == request.Id, disableTracking: true);

            var blog = blogQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.BLOG_NOT_FOUND);

            if (blog.IsActive)
            {
                blog.IsActive = false;
            }
            else
            {
                blog.IsActive = true;
            }

            await _unitOfWork.Blogs.UpdateAsync(blog);

            await _unitOfWork.CompleteAsync();

            return new Response<GetBlogResponse>(code: (int)ResponseCode.DISABLED_SUCCESS, message: ResponseCode.DISABLED_SUCCESS.GetDescription());
        }
    }
}

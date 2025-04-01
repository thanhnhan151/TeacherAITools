using MediatR;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateBlogCommand, Response<GetBlogResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<GetBlogResponse>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            List<string> errorMessages = [];

            var blogQuery = await _unitOfWork.Blogs.GetAsync(expression: u => u.BlogId == request.Id, disableTracking: true);

            var blog = blogQuery.FirstOrDefault();

            if (blog is null)
            {
                errorMessages.Add(ResponseCode.BLOG_NOT_FOUND.GetDescription());
                throw new ValidationException(ResponseCode.BLOG_NOT_FOUND, errorMessages);
            }

            var validator = new UpdateBlogCommandValidator();
            var result = await validator.ValidateAsync(request.UpdateBlogRequest, cancellationToken);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                throw new ValidationException(ResponseCode.CREATED_UNSUCC, errorMessages);
            }

            blog.Title = request.UpdateBlogRequest.Title;
            blog.Body = request.UpdateBlogRequest.Body;

            await _unitOfWork.Blogs.UpdateAsync(blog);
            await _unitOfWork.CompleteAsync();

            return new Response<GetBlogResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}

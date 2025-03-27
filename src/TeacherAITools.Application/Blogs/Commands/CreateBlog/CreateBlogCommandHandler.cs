using MediatR;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandHandler(
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateBlogCommand, Response<GetBlogResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        public async Task<Response<GetBlogResponse>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var newBlog = new Blog
            {
                Title = request.Title,
                Body = request.Body,
                PublicationDate = _dateTimeProvider.UtcNow,
                CategoryId = request.CategoryId
            };

            var result = await _unitOfWork.Blogs.AddAsync(newBlog);

            await _unitOfWork.CompleteAsync();

            return new Response<GetBlogResponse>(code: (int)ResponseCode.CREATED_SUCCESS, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

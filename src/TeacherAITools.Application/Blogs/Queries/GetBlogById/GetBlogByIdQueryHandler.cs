using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Blogs.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Blogs.Queries.GetBlogById
{
    public class GetBlogByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetBlogByIdQuery, Response<GetBlogDetailResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetBlogDetailResponse>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blogQuery = await _unitOfWork.Blogs.GetAsync(b => b.BlogId == request.Id);

            var blog = blogQuery
                .Include(r => r.Category)
                .Include(r => r.Comments.Where(c => c.Status))
                    .ThenInclude(r => r.User)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.BLOG_NOT_FOUND);

            return new Response<GetBlogDetailResponse>(code: (int)ResponseCode.SUCCESS, data: _mapper.Map<GetBlogDetailResponse>(blog), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

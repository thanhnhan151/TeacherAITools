using AutoMapper;
using MediatR;
using TeacherAITools.Application.Categories.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetCategoriesQuery, Response<List<GetCategoryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetCategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoryQuery = await _unitOfWork.Categories.GetAllAsync();

            return new Response<List<GetCategoryResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetCategoryResponse>>(categoryQuery.ToList()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

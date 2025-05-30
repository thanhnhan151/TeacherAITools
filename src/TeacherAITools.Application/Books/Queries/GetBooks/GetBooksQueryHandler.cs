using AutoMapper;
using MediatR;
using TeacherAITools.Application.Books.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetBooksQuery, Response<List<GetBookResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetBookResponse>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var bookQuery = await _unitOfWork.Books.GetAllAsync();

            return new Response<List<GetBookResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetBookResponse>>(bookQuery.ToList()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

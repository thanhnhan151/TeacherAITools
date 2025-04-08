using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Notes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Notes.Queries.GetNotes
{
    public class GetNotesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetNotesQuery, Response<List<GetNoteResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetNoteResponse>>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetNoteResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetNoteResponse>>((await _unitOfWork.Notes.GetAllAsync())),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

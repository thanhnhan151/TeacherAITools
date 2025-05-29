using AutoMapper;
using MediatR;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Queries.GetCurriculumSubSections
{
    public class GetCurriculumSubSectionsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<GetCurriculumSubSectionsQuery, Response<List<GetCurriculumSubSectionsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<List<GetCurriculumSubSectionsResponse>>> Handle(GetCurriculumSubSectionsQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<GetCurriculumSubSectionsResponse>>(code: (int)ResponseCode.SUCCESS,
                data: _mapper.Map<List<GetCurriculumSubSectionsResponse>>(await _unitOfWork.CurriculumSubSections.GetAllAsync()),
                message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}

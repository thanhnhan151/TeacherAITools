using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TeacherAITools.Application.Curriculums.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Curriculums.Commands.CreateCurriculumDetail
{
    public record CreateCurriculumDetailCommand(
        CreateCurriculumDetailRequest createCurriculumDetailRequest) : IRequest<Response<GetDetailCurriculumResponse>>;
}
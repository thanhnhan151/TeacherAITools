using MediatR;
using TeacherAITools.Application.Periods.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Periods.Commands.CreatePeriod
{
    public record CreatePeriodCommand(
        CreatePeriodRequest createPeriodRequest) : IRequest<Response<GetPeriodResponse>>;
}
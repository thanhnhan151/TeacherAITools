using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TeacherAITools.Application.LessonHistories.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.LessonHistories.Commands.CreateLessonHistory
{
    public record CreateLessonHistoryCommand(
        CreateLessonHistoryRequest createLessonHistoryRequest) : IRequest<Response<GetLessonHistoryResponse>>;
}
using MediatR;
using TeacherAITools.Application.Notes.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Notes.Queries.GetNotes
{
    public record GetNotesQuery() : IRequest<Response<List<GetNoteResponse>>>;
}

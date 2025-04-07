using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Prompts.Commands
{
    public class CreatePromptCommandHandler(
        IUnitOfWork unitOfWork) : IRequestHandler<CreatePromptCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(l => l.LessonId == request.LessonId);

            var lesson = lessonQuery
                .Include(l => l.LessonType)
                .Include(l => l.Module)
                        .ThenInclude(l => l.Grade)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            string prompt = $"Soạn nội dung cho bài học: {lesson.Name}, dạng bài: {lesson.LessonType.LessonTypeName}, lớp {lesson.Module.Grade.GradeNumber}, chương: {lesson.Module.Name}";

            var newPrompt = new Prompt
            {
                Description = prompt,
                UserId = 1,
                LessonId = lesson.LessonId
            };

            var result = await _unitOfWork.Prompts.AddAsync(newPrompt);

            await _unitOfWork.CompleteAsync();

            return new Response<string>(code: (int)ResponseCode.CREATED_SUCCESS, data: prompt, message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

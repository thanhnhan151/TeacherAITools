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
        private const string text = "bước đầu nhận biết và sử dụng đúng các từ chỉ vị trí trong không gian, biết diễn đạt chính xác bằng ngôn ngữ, và vận dụng kiến thức vào các tình huống thực tế trong đời sống hàng ngày như đi đường, lên xuống cầu thang, sắp xếp đồ vật… Bài giảng cần đảm bảo các yêu cầu cần đạt:";

        private const string startUpDetail = "Khởi động (gợi sự tò mò qua tranh ảnh và câu hỏi dẫn dắt)";

        private const string knowLedgeDetail = "Hình thành kiến thức (giáo viên hướng dẫn nhận biết và sử dụng từ chỉ vị trí thông qua tranh ảnh, đồ vật thật và ví dụ gần gũi)";

        private const string pracTiceDetail = "Luyện tập (học sinh thực hành cá nhân và theo nhóm, mô tả vị trí, sắp xếp đồ vật theo yêu cầu, tham gia trò chơi như “Làm theo tôi nói – không làm theo tôi làm”)";

        private const string applyDetail = "và Vận dụng (liên hệ với các tình huống thực tế như đi bên phải khi đi đường, giữ tay vịn khi lên – xuống cầu thang, nhận biết vị trí của bạn trong lớp…).";

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(l => l.LessonId == request.LessonId);

            var lesson = lessonQuery
                .Include(l => l.LessonType)
                .Include(l => l.Requirement)
                .Include(l => l.Module)
                        .ThenInclude(l => l.Grade)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            string prompt = $"Soạn nội dung bài giảng: {lesson.Name}, dạng bài: {lesson.LessonType.LessonTypeName}, chương: {lesson.Module.Name}. Bài giảng nhằm giúp học sinh lớp {lesson.Module.Grade.GradeNumber} {text} {lesson.Requirement.Description} Bài giảng triển khai theo tiến trình sư phạm: {startUpDetail}, {knowLedgeDetail}, {pracTiceDetail}, {applyDetail}";

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

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
        private const string startUp = "1. *Trải nghiệm*:";

        private const string startUpDetail = "- Mục đích của hoạt động này là tạo tâm thế, giúp học sinh ý thức được nhiệm vụ học tập. Giáo viên không nên thông báo ngay các kiến thức có sẵn mà cần tạo ra các tình huống gợi vấn đề để học sinh huy động kiến thức, kinh nghiệm của bản thân và suy nghĩ để tìm hướng giải quyết.";

        private const string knowLedge = "2. *Hình thành kiến thức mới*:";

        private const string knowLedgeDetail = "- Mục đích của hoạt động này nhằm giúp học sinh phát hiện, chiếm lĩnh được kiến thức và kỹ năng mới. Giáo viên sẽ giúp học sinh huy động kiến thức, chia sẻ và hợp tác trong học tập để xây dựng kiến thức mới.";

        private const string pracTice = "3. *Thực hành, Luyện tập*:";

        private const string pracTiceDetail = "- Mục đích của hoạt động này nhằm giúp học sinh củng cố và hoàn thiện kiến thức, kỹ năng vừa lĩnh hội và huy động, liên kết với kiến thức đã có để áp dụng vào giải quyết vấn đề.";

        private const string apply = "4. *Vận dụng*:";

        private const string applyDetail = "- Mục đích của hoạt động này là giúp học sinh vận dụng các kiến thức và kỹ năng đã học vào giải quyết các vấn đề có tính chất thực tiễn. Bạn có thể đưa ra các câu hỏi hoặc dự án học tập nhỏ để học sinh thực hiện theo hoạt động cá nhân hoặc nhóm. Hoạt động này có thể được tổ chức ngoài giờ học chính khóa. Giáo viên cũng nên khuyến khích học sinh tiếp tục tìm tòi và mở rộng kiến thức sau khi kết thúc bài học.";

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CreatePromptCommand request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(l => l.LessonId == request.LessonId);

            var lesson = lessonQuery
                .Include(l => l.LessonType)
                .Include(l => l.Module)
                        .ThenInclude(l => l.Grade)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            string prompt = $"Soạn nội dung cho bài học: {lesson.Name}, dạng bài: {lesson.LessonType.LessonTypeName}, lớp {lesson.Module.Grade.GradeNumber}, chương: {lesson.Module.Name} theo cấu trúc sau:\n\n{startUp}\n{startUpDetail}\n\n{knowLedge}\n{knowLedgeDetail}\n\n{pracTice}\n{pracTiceDetail}\n\n{apply}\n{applyDetail}";

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

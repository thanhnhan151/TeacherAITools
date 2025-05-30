using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Entities;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.CreateTeacherLesson
{
    public class CreateTeacherLessonCommandHandler(
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateTeacherLessonCommand, Response<GetTeacherLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        public async Task<Response<GetTeacherLessonResponse>> Handle(CreateTeacherLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonQuery = await _unitOfWork.Lessons.GetAsync(lesson => lesson.LessonId == request.LessonId);

            var lesson = lessonQuery
                .Include(l => l.StartUp)
                .Include(l => l.KnowLedge)
                .Include(l => l.Practice)
                .Include(l => l.Apply)
                .Include(l => l.Module)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.LESSON_NOT_FOUND);

            #region Prompt

            var firstPart = $"Hãy tạo giáo án môn Toán lớp {lesson.Module.GradeId} theo đúng chuẩn Thông tư 27 và Công văn 2345 của Bộ Giáo dục và Đào tạo Việt Nam. Bài học thuộc Chủ đề: {lesson.Module.Name}, có tiêu đề {lesson.Name}. ";

            var secondPart = $"Giáo án gồm ba phần chính. Phần I – Yêu cầu cần đạt: trình bày rõ 2–3 năng lực đặc thù của môn Toán như {lesson.SpecialAbility}; 2–3 năng lực chung như {lesson.GeneralCapacity}; cùng 1–2 phẩm chất {lesson.Quality}. Phần II – Đồ dùng dạy học: {lesson.SchoolSupply}. ";

            var thirdPart = $"Phần III – Các hoạt động dạy học chủ yếu: trình bày 4 hoạt động chính với thời lượng không vượt quá {lesson.Duration} phút, mỗi hoạt động đều phải thể hiện rõ phần “HOẠT ĐỘNG CỦA GIÁO VIÊN” và “HOẠT ĐỘNG CỦA HỌC SINH”.";

            var fourthPart = $" Cụ thể, A – Hoạt động MỞ ĐẦU {lesson.StartUp.Duration}: mục tiêu {lesson.StartUp.Goal}, HOẠT ĐỘNG CỦA GIÁO VIÊN: {lesson.StartUp.TeacherActivities}. HOẠT ĐỘNG CỦA HỌC SINH: {lesson.StartUp.StudentActivities}. ";

            var fifthPart = $"B – Hoạt động HÌNH THÀNH KIẾN THỨC {lesson.KnowLedge.Duration}: {lesson.KnowLedge.Goal}, HOẠT ĐỘNG CỦA GIÁO VIÊN: {lesson.KnowLedge.TeacherActivities}. HOẠT ĐỘNG CỦA HỌC SINH: {lesson.KnowLedge.StudentActivities}. ";

            var sixthPart = $"C – Hoạt động LUYỆN TẬP, THỰC HÀNH {lesson.Practice.Duration}: mục tiêu {lesson.Practice.Goal}, HOẠT ĐỘNG CỦA GIÁO VIÊN: {lesson.KnowLedge.TeacherActivities}. HOẠT ĐỘNG CỦA HỌC SINH: {lesson.Practice.StudentActivities}. ";

            var seventhPart = $"D – Hoạt động VẬN DỤNG, TRẢI NGHIỆM {lesson.Apply.Duration}: {lesson.Apply.Goal}, HOẠT ĐỘNG CỦA GIÁO VIÊN: {lesson.Apply.TeacherActivities}. HOẠT ĐỘNG CỦA HỌC SINH: {lesson.Apply.StudentActivities}.";

            #endregion

            var newPrompt = new Prompt
            {
                Description = firstPart + secondPart + thirdPart + fourthPart + firstPart + sixthPart,
                CreatedAt = _dateTimeProvider.UtcNow,
                LessonId = request.LessonId,
                UserId = request.UserId
            };

            await _unitOfWork.Prompts.AddAsync(newPrompt);

            await _unitOfWork.CompleteAsync();


            TeacherLesson teacherLesson = new()
            {
                StartUp = request.StartUp,
                Knowledge = request.KnowLedge,
                Practice = request.Practice,
                Apply = request.Apply,
                Goal = request.Goal,
                SchoolSupply = request.SchoolSupply,
                UserId = request.UserId,
                PromptId = _unitOfWork.Prompts.GetLastIdPrompt(),
                CreatedAt = _dateTimeProvider.UtcNow
            };

            await _unitOfWork.TeacherLessons.AddAsync(teacherLesson);

            await _unitOfWork.CompleteAsync();

            return new Response<GetTeacherLessonResponse>(code: (int)ResponseCode.CREATED_SUCCESS,
                message: ResponseCode.CREATED_SUCCESS.GetDescription());
        }
    }
}

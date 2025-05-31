using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Application.Common.Models.Requests;
using TeacherAITools.Application.TeacherLessons.Common;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.TeacherLessons.Commands.UpdateStatusTeacherLesson
{
    public class UpdateStatusTeacherLessonCommandHandler(
        IUnitOfWork unitOfWork,
        IEmailService emailService) : IRequestHandler<UpdateStatusTeacherLessonCommand, Response<GetDetailTeacherLessonResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IEmailService _emailService = emailService;

        public async Task<Response<GetDetailTeacherLessonResponse>> Handle(UpdateStatusTeacherLessonCommand request, CancellationToken cancellationToken)
        {
            var mailRequest = new MailRequest();

            var query = await _unitOfWork.TeacherLessons.GetAsync(expression: m => m.LessonPlanId == request.Id, disableTracking: true);

            var teacherLesson = query
                .Include(teacherLesson => teacherLesson.User)
                .Include(teacherLesson => teacherLesson.Prompt)
                .ThenInclude(teacherLesson => teacherLesson.Lesson)
                .FirstOrDefault() ?? throw new ApiException(ResponseCode.TEACHER_LESSON_DONT_EXIST);

            teacherLesson.Status = request.updateStatusTeacherLessonRequest.Status;

            switch (request.updateStatusTeacherLessonRequest.Status)
            {
                case Domain.Common.LessonStatus.Rejected:
                    if (string.IsNullOrEmpty(request.updateStatusTeacherLessonRequest.DisapprovedReason))
                    {
                        throw new ApiException(ResponseCode.MUST_HAVE_DISAPPROVED_REASON);
                    }

                    teacherLesson.RejectedCount += 1;

                    teacherLesson.DisapprovedReason = request.updateStatusTeacherLessonRequest.DisapprovedReason;

                    mailRequest.ToEmail = teacherLesson.User.Email;

                    mailRequest.Subject = "BÀI GIẢNG CỦA BẠN ĐÃ BỊ TỪ CHỐI";

                    mailRequest.Body = $"Hệ thống Math AI Tools xin thông báo:\r\n\r\nThông tin chi tiết:\r\n\r\nBài học: {teacherLesson.Prompt.Lesson.Name}\r\n\r\nGiảng viên sử dụng: {teacherLesson.User.Fullname}\r\n\r\nThời gian: {teacherLesson.CreatedAt.GetFormatDateTime()}\r\n\r\nNội dung thông báo: Bài giảng của bạn đã được Quản lý chuyên môn từ chối với lý do: {teacherLesson.DisapprovedReason}\r\n\r\nTrân trọng,\r\nHệ thống AI Math Tools";

                    await _emailService.SendEmailAsync(mailRequest);
                    break;
                case Domain.Common.LessonStatus.Draft:
                    teacherLesson.DisapprovedReason = string.Empty;
                    break;
                case Domain.Common.LessonStatus.Pending:
                    break;
                case Domain.Common.LessonStatus.Approved:
                    mailRequest.ToEmail = teacherLesson.User.Email;

                    mailRequest.Subject = "BÀI GIẢNG CỦA BẠN ĐÃ ĐƯỢC PHÊ DUYỆT";

                    mailRequest.Body = $"Hệ thống Math AI Tools xin thông báo:\r\n\r\nThông tin chi tiết:\r\n\r\nBài học: {teacherLesson.Prompt.Lesson.Name}\r\n\r\nGiảng viên sử dụng: {teacherLesson.User.Fullname}\r\n\r\nThời gian: {teacherLesson.CreatedAt.GetFormatDateTime()}\r\n\r\nNội dung thông báo: Bài giảng của bạn đã được Quản lý chuyên môn phê duyệt\r\n\r\nTrân trọng,\r\nHệ thống AI Math Tools";

                    await _emailService.SendEmailAsync(mailRequest);
                    break;
            }

            await _unitOfWork.TeacherLessons.UpdateAsync(teacherLesson);
            await _unitOfWork.CompleteAsync();

            return new Response<GetDetailTeacherLessonResponse>(code: (int)ResponseCode.UPDATED_SUCCESS, message: ResponseCode.UPDATED_SUCCESS.GetDescription());
        }
    }
}

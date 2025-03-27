namespace TeacherAITools.Application.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetFormatDateTime(this DateTime dateTime)
        {
            return dateTime.AddHours(7).ToString("dd/MM/yyyy H:mm");
        }
    }
}

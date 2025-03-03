using System.ComponentModel;

namespace TeacherAITools.Domain.Common
{
    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female,
        [Description("Others")]
        Others
    }
}

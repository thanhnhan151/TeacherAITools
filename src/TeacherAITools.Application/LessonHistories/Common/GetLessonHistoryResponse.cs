using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherAITools.Application.LessonHistories.Common
{
    public class GetLessonHistoryResponse
    {
        public int Id { get; set; }
        public string StartUp { get; set; } = string.Empty;
        public string Knowledge { get; set; } = string.Empty;
        public string Practice { get; set; } = string.Empty;
        public string Apply { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public string SchoolSupply { get; set; } = string.Empty;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherAITools.Application.Curriculums.Common
{
    public class CreateCurriculumDetailRequest
    {
        public string CurriculumContent { get; set; } = string.Empty;
        public string CurriculumGoal { get; set; } = string.Empty;
        public int? CurriculumId { get; set; }
        public int? CurriculumSubSectionId { get; set; }
    }
}
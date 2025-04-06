using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherAITools.Application.Periods.Common
{
    public class GetPeriodResponse
    {
        public int PeriodId { get; set; }
        public int Number { get; set; }
        public List<PeriodDetailResponse>? PeriodDetailResponses {get; set;}
    }

    public class PeriodDetailResponse{
        public int PeriodDetailId { get; set; }
        public string StartUp { get; set; } = string.Empty;
        public string Knowledge { get; set; } = string.Empty;
        public string Practice { get; set; } = string.Empty;
        public string Apply { get; set; } = string.Empty;
    }
}
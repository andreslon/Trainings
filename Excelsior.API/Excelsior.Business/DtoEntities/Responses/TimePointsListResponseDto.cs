using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class TimePointsListResponseDto
    {
        public long? TimePointsListID { get; set; }
        public long? TrialID { get; set; }
        public string TimePointsDescription { get; set; }
        public int? TimePointsSeq { get; set; }
        public bool? IsInitialTimePoint { get; set; }
        public bool? IsTerminalTimePoint { get; set; }
        public bool? IsEligibilityTimePoint { get; set; }
        public int? ExpectedVisitStartDay { get; set; }
        public int? ExpectedVisitEndDay { get; set; }
    }
}

using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Request
{
    public class GradingReportsRequestDto : BaseRequestDto
    {
        public long? SeriesId { get; set; }
        public long? PerformedById { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsSigned { get; set; }
    }
}

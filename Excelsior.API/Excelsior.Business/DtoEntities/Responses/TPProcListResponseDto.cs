using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class TPProcListResponseDto
    {
        public long? TPProcID { get; set; }
        public long? TimePointsListID { get; set; }
        public long? ImgProcedureID { get; set; }
        public long? WFTemplateID { get; set; }
        public long? GTemplateID { get; set; }
        public bool? IsGradeBothLaterality { get; set; }
        public int? PercentSeriesForReview { get; set; }
        public int? CounterSeriesForReview { get; set; }
        public int? CounterSeriesSigned { get; set; }
        public ImgProcedureResponseDto ImgProcedure {get; set;}
    }
}
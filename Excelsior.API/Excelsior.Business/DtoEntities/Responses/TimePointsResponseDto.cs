using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class TimePointsResponseDto
    {
        public long? TimePointsID { get; set; }
        public long? SubjectID { get; set; }
        public long? TimePointsListID { get; set; }
        public string TimePointsDCMInstanceUID { get; set; }
        public TimePointsListResponseDto TimePointsList { get; set; }
        public SubjectsResponseDto Subject { get; set; }
    }
}

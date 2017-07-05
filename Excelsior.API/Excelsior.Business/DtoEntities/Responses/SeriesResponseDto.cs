using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class SeriesResponseDto
    {
        public long SeriesID { get; set; }
        public long? TimePointsID { get; set; }
        public long? TPProcListID { get; set; }
        public bool? DirectorReviewComplete { get; set; }
        public bool IsActive { get; set; }
        public bool IsValidated { get; set; }
        public bool IsQCSeries { get; set; }
        public long? PhotographerID { get; set; }
        public long? EquipmentID { get; set; }
        public DateTime? LastExportDateTime { get; set; }
        public string SeriesDCMInstanceUID { get; set; }
        public bool IsDataQualityAdequate { get; set; }
        public DateTime? StudyDate { get; set; }
        public long? SeriesGroupID { get; set; }

        public TimePointsResponseDto TimePoint { get; set; }
        public TPProcListResponseDto TPProcList { get; set; }      
    }
}

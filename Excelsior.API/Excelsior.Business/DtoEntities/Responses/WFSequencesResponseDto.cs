using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class WFSequencesResponseDto
    {
        public string CategoryDes { get; set; }
        public long? SeriesGroupID { get; set; }
        public DateTime? LastStepCompletionDate { get; set; }
        public string SiteName { get; set; }
        public string RandomizedSubjectID { get; set; }
        public string AlternativeRandomizedSubjectID { get; set; }
        public string NameCode { get; set; }
        public string Laterality { get; set; }
        public DateTime? StudyDate { get; set; }
        public string TimePointsDescription { get; set; }
        public string ImgProcedureName { get; set; }
        public string ContactUserName { get; set; }
        public string ContactEquipmentName { get; set; }
        public string WFStep { get; set; }
        public string AssignedToName { get; set; }
        public long? AssignedToID { get; set; }
        public long? SeriesID { get; set; }
        public bool? IsTechnicianCerified { get; set; }
        public bool? IsTestingSubject { get; set; }
        public bool? IsEquipmentCerified { get; set; }
        public TimePointsResponseDto TimePoint { get; set; }
        public TPProcListResponseDto TPProcList { get; set; }
        public string SegmentationStatus { get; set; }
        public bool? IsDataQualityAdequate { get; set; }
        public int? TotalUploads { get; set; }
    }
}

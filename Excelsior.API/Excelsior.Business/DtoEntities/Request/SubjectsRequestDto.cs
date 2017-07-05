using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Request
{
    public class SubjectsRequestDto : BaseRequestDto
    {
        public long? StudyId { get; set; }
        public long? SiteId { get; set; }
        public long? AffiliationId { get; set; }
        public long? GroupId { get; set; }
        public long? CohortId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsRejected { get; set; }
    }
}
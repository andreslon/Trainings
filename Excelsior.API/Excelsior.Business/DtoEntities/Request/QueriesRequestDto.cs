namespace Excelsior.Business.DtoEntities.Request
{
    public class QueriesRequestDto : BaseRequestDto
    {
        public bool? IsActive { get; set; }
        public string QueryType { get; set; }
        public string QueryStatus { get; set; }
        public long? StudyId { get; set; }
        public long? SiteId { get; set; }
        public long? SeriesId { get; set; }
        public long? CertUserId { get; set; }
        public long? CertEquipmentId { get; set; }
    }
}

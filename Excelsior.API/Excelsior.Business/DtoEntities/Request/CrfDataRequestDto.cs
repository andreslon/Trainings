namespace Excelsior.Business.DtoEntities.Request
{
    public class CrfDataRequestDto : BaseRequestDto
    {
        public long? StudyId { get; set; }
        public long? SeriesId { get; set; }
        public long? SubjectId { get; set; }
        public long? TimePointId { get; set; }
        public long? ProcedureId { get; set; }
        public bool? IsActive { get; set; }
    }
}

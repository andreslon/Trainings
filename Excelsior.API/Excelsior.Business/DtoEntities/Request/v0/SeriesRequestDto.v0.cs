namespace Excelsior.Business.DtoEntities.Request.v0
{
    public class SeriesRequestDto : BaseRequestDto
    {
        public long StudyId { get; set; }
        public string Step { get; set; }
        public bool? Assigned { get; set; }
        public long? SubjectId { get; set; }
        public long? TimePointListId { get; set; }
        public long? ProcedureId { get; set; }
    }
}
namespace Excelsior.Business.DtoEntities.Request
{
    public class SeriesRequestDto : BaseRequestDto
    {
        public long? CategoryId { get; set; }
        public string DataType { get; set; }
        public long StudyId { get; set; }
        public string Step { get; set; }
        public long? SiteId { get; set; }
        public long? SubjectId { get; set; }
        public long? TimePointListId { get; set; }
        public long? ProcedureId { get; set; }
        public string AssignedTo { get; set; }
        public long? SeriesGroupId { get; set; }
        public long? SubjectGroupId { get; set; }
        public long? SubjectCohortId { get; set; }
    }
}
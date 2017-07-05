namespace Excelsior.Business.DtoEntities.Request
{
    public class VisitMatrixProceduresRequestDto : BaseRequestDto
    {
        public long? SiteId { get; set; }

        public long? SubjectId { get; set; }

        public long? TimePointId { get; set; }
    }
}
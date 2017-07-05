namespace Excelsior.Business.DtoEntities.Request
{
    public class VisitMatrixSubjectsRequestDto : BaseRequestDto
    {
        public long? SiteId { get; set; }

        public long? TimePointId { get; set; }

        public long? ProcedureId { get; set; }

        public long? StepId { get; set; }
    }
}
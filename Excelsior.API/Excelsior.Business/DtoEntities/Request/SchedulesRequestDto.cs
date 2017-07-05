namespace Excelsior.Business.DtoEntities.Request
{
    public class SchedulesRequestDto : BaseRequestDto
    {
        public long? StudyId { get; set; }
        public long? TimePointId { get; set; }
        public long? ProcedureId { get; set; }
        public long? SubjectId { get; set; }
    }
}

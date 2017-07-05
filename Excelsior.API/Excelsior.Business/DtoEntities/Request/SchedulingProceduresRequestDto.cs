namespace Excelsior.Business.DtoEntities.Request
{
    public class SchedulingProceduresRequestDto : BaseRequestDto
    {
        public long StudyId { get; set; }
        public bool? Scheduled { get; set; }
    }
}
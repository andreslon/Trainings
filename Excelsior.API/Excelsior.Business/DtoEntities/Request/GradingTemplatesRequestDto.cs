namespace Excelsior.Business.DtoEntities.Request
{
    public class GradingTemplatesRequestDto : BaseRequestDto
    {
        public long? StudyId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
    }
}
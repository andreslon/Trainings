namespace Excelsior.Business.DtoEntities.Request
{
    public class TemplateQuestionsRequestDto : BaseRequestDto
    { 
        public long? TemplateId { get; set; }
        public bool? IsActive { get; set; }
    }
}

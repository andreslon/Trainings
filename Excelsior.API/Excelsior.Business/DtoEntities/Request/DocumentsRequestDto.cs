namespace Excelsior.Business.DtoEntities.Request
{
    public class DocumentsRequestDto : BaseRequestDto
    {
        public bool? IsActive { get; set; }

        public long? StudyId { get; set; }
    }
}

namespace Excelsior.Business.DtoEntities.Request
{
    public class DocumentVersionsRequestDto : BaseRequestDto
    {
        public bool? IsActive { get; set; }

        public long? StudyId { get; set; }

        public long? DocumentId { get; set; }
    }
}

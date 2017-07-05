namespace Excelsior.Business.DtoEntities.Request
{
    public class DocumentsVersonRequestDto : BaseRequestDto
    {
        public bool? IsActive { get; set; }

        public long? StudyId { get; set; }

        public long? DocumentId { get; set; }
    }
}

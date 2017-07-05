namespace Excelsior.Business.DtoEntities.Request
{
    public class ProcessedDataRequestDto : BaseRequestDto
    {
        public long MediaId { get; set; }
        public string CurrentUserId { get; set; }
    }
}

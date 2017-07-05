namespace Excelsior.Business.DtoEntities.Request
{
    public class QueryMessagesRequestDto : BaseRequestDto
    {
        public bool? IsActive { get; set; }
        public long? QueryId { get; set; }
    }
}

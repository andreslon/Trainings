namespace Excelsior.Business.DtoEntities.Request
{
    public class AttachementsRequestDto : BaseRequestDto
    {
        public bool? IsActive { get; set; }
        public long SeriesId { get; set; }
        public string Laterality { get; set; }
    }
}
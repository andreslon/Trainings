namespace Excelsior.Business.DtoEntities.Request
{
    public class UploadsRequestDto : BaseRequestDto
    {
        public bool? IsActive { get; set; }
        public long? SeriesId { get; set; }
    }
}
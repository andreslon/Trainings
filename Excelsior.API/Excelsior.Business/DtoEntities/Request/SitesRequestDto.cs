namespace Excelsior.Business.DtoEntities.Request
{
    public class SitesRequestDto: BaseRequestDto
    {
        public long? StudyId { get; set; }
        public bool? IsActive { get; set; }
    }
}
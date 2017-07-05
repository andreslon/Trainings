namespace Excelsior.Business.DtoEntities.Request.v0
{
    public class TrialsRequestDto: BaseRequestDto
    {
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
    }
}
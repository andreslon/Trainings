namespace Excelsior.Business.DtoEntities.Request
{
    public class StudiesRequestDto: BaseRequestDto
    {
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }          
    }
}
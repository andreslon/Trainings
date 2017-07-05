namespace Excelsior.Business.DtoEntities.Request
{
    public class StudyUserRequestDto : BaseRequestDto
    {
        public long? Id { get; set; }
        public long? StudyId { get; set; }
        public bool? IsActive { get; set; }
    }
}

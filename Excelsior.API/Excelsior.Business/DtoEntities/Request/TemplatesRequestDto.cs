namespace Excelsior.Business.DtoEntities.Request
{
    public class TemplatesRequestDto : BaseRequestDto
    {
        public long Id { get; set; }
        public long? StudyId { get; set; }
        public long? TimePointId { get; set; }
        public long? ProcedureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbrev { get; set; }
        public bool? IsAssocTimePoint { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
    }
}

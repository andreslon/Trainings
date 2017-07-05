namespace Excelsior.Business.DtoEntities.Request
{
    public class CertUserRequestDto : BaseRequestDto
    {
        public long? Id { get; set; }
        public long? StudyId { get; set; }
        public long? TechnicianId { get; set; }        
        public long? AffiliationId { get; set; }
        public long? ProcedureId { get; set; }
        public bool? IsCertified { get; set; }
        public bool? IsActive { get; set; }
        public bool? HasPrevCert { get; set; }
        public long? CertifiedById { get; set; }
        public string AssignedTo { get; set; }
    }
}

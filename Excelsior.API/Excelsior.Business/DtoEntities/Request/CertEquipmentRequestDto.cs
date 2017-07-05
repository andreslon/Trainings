namespace Excelsior.Business.DtoEntities.Request
{
    public class CertEquipmentRequestDto : BaseRequestDto
    {
        public long? StudyId { get; set; }
        public long? AffiliationId { get; set; }
        public long? ProcedureId { get; set; }
        public long? EquipmentId { get; set; }
        public bool? IsCertified { get; set; }
        public bool? IsActive { get; set; }
        public bool? HasPrevCert { get; set; }
        public long? CertifiedById { get; set; }
        public string AssignedTo { get; set; }
    }
}

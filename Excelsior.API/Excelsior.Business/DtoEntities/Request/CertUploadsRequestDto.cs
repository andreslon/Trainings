namespace Excelsior.Business.DtoEntities.Request
{
    public class CertUploadsRequestDto : BaseRequestDto
    {
        public bool? IsCertified { get; set; }
        public bool? IsActive { get; set; }
        public long? CertEquipmentId { get; set; }
        public long? CertUserId { get; set; }
    }
}
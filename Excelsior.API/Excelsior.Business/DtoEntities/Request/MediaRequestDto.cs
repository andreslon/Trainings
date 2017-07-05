using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Request
{
    public class MediaRequestDto : BaseRequestDto
    {
        public bool? IsActive { get; set; }
        public string DataType { get; set; }
        public long? SeriesId { get; set; }
        public long? CertUserId { get; set; }
        public long? CertEquipmentId { get; set; }
        public IList<long> Ids { get; set; }
    }
}
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Request.v0
{
    public class CommonRequestDto : BaseRequestDto
    {
        public long CommonId { get; set; }
        public List<long> CommonList { get; set; }
    }
}

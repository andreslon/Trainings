using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Request
{
    public class CommonRequestDto : BaseRequestDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public IList<long> List { get; set; }
    }
}

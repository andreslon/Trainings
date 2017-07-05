using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class AffiliationsResponseDto
    {
        public long? AffiliationID { get; set; }
        public string AffiliationName { get; set; }
        public bool? IsActive { get; set; }
        public string Country { get; set; }
    }
}

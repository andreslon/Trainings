using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class SitesResponseDto
    {
        public long? SiteID { get; set; }
        public string RandomizedSiteID { get; set; }
        public long? TrialID { get; set; }
        public long? AffiliationID { get; set; }
        public bool? IsIRB { get; set; }
        public bool? IsTestingSite { get; set; }
        public bool? IsActive { get; set; }
        public string PrincipalInvestigator { get; set; }
        public AffiliationsResponseDto Affiliation { get; set; }
    }
}

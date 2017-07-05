using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_Site.PACS_SiteMetadata))]
    public partial class PACS_Site
    {
        internal sealed class PACS_SiteMetadata
        {
            [Association("Trial_Association", "TrialID", "TrialID")]
            
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }

            [Association("Affiliation_Association", "AffiliationID", "AffiliationID")]
            
            public CONTACT_Affiliation CONTACTAffiliation
            {
                get;
                set;
            }
        }
    }
}
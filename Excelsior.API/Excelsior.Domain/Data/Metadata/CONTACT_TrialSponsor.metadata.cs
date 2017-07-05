using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_TrialSponsor.CONTACT_TrialSponsorMetadata))]
    public partial class CONTACT_TrialSponsor
    {
        internal sealed class CONTACT_TrialSponsorMetadata
        {
            [Association("TrialSponsor_Affiliation_Association","AffiliationID","AffiliationID")]
            
            public CONTACT_Affiliation CONTACTAffiliation
            {
                get;
                set;
            }
        }
    }
}
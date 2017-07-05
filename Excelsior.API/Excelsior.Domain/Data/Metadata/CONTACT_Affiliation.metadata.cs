using System.ComponentModel.DataAnnotations;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_Affiliation.CONTACT_AffiliationMetadata))]
    public partial class CONTACT_Affiliation
    {
        internal sealed class CONTACT_AffiliationMetadata
        {
            [Required(ErrorMessage = "Name is required.")]
            public string AffiliationName
            {
                get;
                set;
            }

            [Association("CONTACTCountries_Association", "CountryID", "CountryID")]
            public CONTACT_Country CONTACTCountry
            {
                get;
                set;
            }
        }
    }
}
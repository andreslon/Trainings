using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [XmlInclude(typeof(PACS_TrialKeyMetric))]
    [KnownType(typeof(PACS_TrialKeyMetric))]
    [MetadataTypeAttribute(typeof(PACS_Trial.PACS_TrialMetadata))]
    public partial class PACS_Trial
    {
        internal sealed class PACS_TrialMetadata
        {
            [Required(ErrorMessage = "Name is required.")]
            public string TrialName
            {
                get;
                set;
            }

            [Association("CFG_AnimalSpecies_Association", "AnimalSpeciesID", "AnimalSpeciesID")]
            
            public CFG_AnimalSpecy CFGAnimalSpecy
            {
                get;
                set;
            }

            [Association("GRD_Impression_Association", "ImpressionID", "ImpressionID")]
            
            public GRD_Impression GRDImpression
            {
                get;
                set;
            }

            [Association("CONTACT_TrialSponsors_Association", "TrialID", "TrialID")]
            
            public IList<CONTACT_TrialSponsor> CONTACT_TrialSponsors
            {
                get;
                set;
            }

        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(MEA_Stencil.MEA_StencilMetadata))]
    public partial class MEA_Stencil
    {
        internal class MEA_StencilMetadata
        {
            [Required(ErrorMessage = "Color is required.")]
            public string Color
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Tag is required.")]
            public string Tag
            {
                get;
                set;
            }

            [Association("PACSTrial_Association", "TrialID", "TrialID")]
            
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }
        }
    }
}
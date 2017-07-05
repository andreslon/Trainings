using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(WF_Sequence.WF_SequenceMetadata))]
    public partial class WF_Sequence : PACS_Series
    {
        internal sealed class WF_SequenceMetadata : PACS_Series.PACS_SeriesMetadata
        {
            [Association("Sequence_User_Association", "AssignedToID", "UserID")]
            
            public CONTACT_User AssignedTo
            {
                get;
                set;
            }

            [Association("Sequence_WFTempStep_Association", "WFTempStepID", "WFTempStepID")]
            
            public WF_TempStep WFTempStep
            {
                get;
                set;
            }

            [Association("Sequence_CategoryFlag_Association", "CategoryFlagID", "CategoryFlagID")]
            
            public WF_CategoryFlag WFCategoryFlag
            {
                get;
                set;
            }
        }
    }
}
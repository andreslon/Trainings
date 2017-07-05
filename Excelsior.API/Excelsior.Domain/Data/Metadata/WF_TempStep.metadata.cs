using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(WF_TempStep.WF_TempStepMetadata))]
    public partial class WF_TempStep
    {
        internal sealed class WF_TempStepMetadata
        {
            [Association("WFTempStep_WFStepList_Association", "WFStepListID", "WFStepListID")]
            
            public WF_StepList WFStepList
            {
                get;
                set;
            }

            [Association("WFTempStep_WFTemplate_Association", "WFTemplateID", "WFTemplateID")]
            
            public WF_Template WFTemplate
            {
                get;
                set;
            }
        }
    }
}
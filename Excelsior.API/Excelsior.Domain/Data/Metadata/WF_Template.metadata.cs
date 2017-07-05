using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(WF_Template.WF_TemplateMetadata))]
    public partial class WF_Template
    {
        internal sealed class WF_TemplateMetadata
        {
            [Required(ErrorMessage = "Name is required.")]
            public string WFTemplateName
            {
                get;
                set;
            }

            [Association("WF_Template_PACSTrial_Association", "TrialID", "TrialID")]
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }

            [Association("WFTemplate_WFTempSteps_Association", "WFTemplateID", "WFTemplateID")]
            
            public IList<WF_TempStep> WF_TempSteps
            {
                get;
                set;
            }
        }
    }
}
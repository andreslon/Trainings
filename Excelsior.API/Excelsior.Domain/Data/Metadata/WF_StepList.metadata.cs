using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(WF_StepList.WF_StepListMetadata))]
    public partial class WF_StepList
    {
        internal sealed class WF_StepListMetadata
        {
            [Required(ErrorMessage = "Name is required")]
            public string WFStepListDes { get; set; }
        }
    }
}
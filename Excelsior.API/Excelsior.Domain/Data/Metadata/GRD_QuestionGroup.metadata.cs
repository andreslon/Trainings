using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_QuestionGroup.GRD_QuestionGroupMetadata))]
    public partial class GRD_QuestionGroup
    {
        internal sealed class GRD_QuestionGroupMetadata
        {
            [Association("GRDTemplate_Association", "GTemplateID", "GTemplateID")]
            
            public GRD_GradingTemplate GRDGradingTemplate
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Name is required")]
            public string GQuestionGroupName { get; set; }
        }
    }
}
using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_QuestionTag.GRD_QuestionTagMetadata))]
    public partial class GRD_QuestionTag
    {
        internal sealed class GRD_QuestionTagMetadata
        {
            [Required(ErrorMessage = "Name is required")]
            public string GQuestionTagString { get; set; }
        }
    }
}
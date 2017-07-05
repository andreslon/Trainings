using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_GradingAnswer.GRD_GradingAnswerMetadata))]
    public partial class GRD_GradingAnswer
    {
        internal sealed class GRD_GradingAnswerMetadata
        {
            [Required(ErrorMessage = "Answer is required")]
            public string GAnswerString { get; set; }

            [Association("GRD_GradingAnswer_GRDGradingQuestion_Association", "GQuestionID", "GQuestionID")]
            
            public GRD_GradingQuestion GRDGradingQuestion
            {
                get;
                set;
            }

            [Association("GRD_GradingAnswer_GRD_Dependencies_Association", "GAnswersID", "GSourceAnswerID")]
            
            public IList<GRD_Dependency> GRD_Dependencies
            {
                get;
                set;
            }
        }
    }
}
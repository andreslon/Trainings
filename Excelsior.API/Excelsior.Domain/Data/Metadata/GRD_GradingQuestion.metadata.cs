using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_GradingQuestion.GRD_GradingQuestionMetadata))]
    public partial class GRD_GradingQuestion
    {
        internal sealed class GRD_GradingQuestionMetadata
        {
            [Required(ErrorMessage = "Question is required")]
            public string GQuestionString { get; set; }

            [Required(ErrorMessage = "Tag is required")]
            public string GQuestionTagID { get; set; }

            [Association("GRD_GradingAnswers_Association", "GQuestionID", "GQuestionID")]
            
            public IList<GRD_GradingAnswer> GRD_GradingAnswers
            {
                get;
                set;
            }

            [Association("GRD_Dependencies_Association", "GQuestionID", "GTargetQuestionID")]
            
            public IList<GRD_Dependency> GRD_Dependencies
            {
                get;
                set;
            }

            [Association("GRD_GradingQuestion_GRDQuestionTag_Association", "GQuestionTagID", "GQuestionTagID")]
            
            public GRD_QuestionTag GRDQuestionTag
            {
                get;
                set;
            }
        }
    }
}
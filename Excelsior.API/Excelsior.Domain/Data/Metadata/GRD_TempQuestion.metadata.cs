using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_TempQuestion.GRD_TempQuestionMetadata))]
    public partial class GRD_TempQuestion
    {
        internal sealed class GRD_TempQuestionMetadata
        {
            [Association("GRDGradingQuestion_Association", "GQuestionID", "GQuestionID")]
            
            public GRD_GradingQuestion GRDGradingQuestion
            {
                get;
                set;
            }

            [Association("GRDQuestionGroup_Association", "GQuestionGroupID", "GQuestionGroupID")]
            
            public GRD_QuestionGroup GRDQuestionGroup
            {
                get;
                set;
            }
        }
    }
}
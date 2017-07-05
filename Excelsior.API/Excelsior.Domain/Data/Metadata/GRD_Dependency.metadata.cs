using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_Dependency.GRD_DependencyMetadata))]
    public partial class GRD_Dependency
    {
        internal sealed class GRD_DependencyMetadata
        {
            [Association("GRDSourceGradingAnswer_Association", "GSourceAnswerID", "GAnswersID")]
            
            public GRD_GradingAnswer GRDGradingAnswer
            {
                get;
                set;
            }

            [Association("GRDTargetGradingAnswer_Association", "GTargetAnswerID", "GAnswersID")]
            
            public GRD_GradingAnswer GRDGradingAnswer1
            {
                get;
                set;
            }

            [Association("GRDTargetGradingQuestion_Association", "GTargetQuestionID", "GQuestionID")]
            
            public GRD_GradingQuestion GRDGradingQuestion
            {
                get;
                set;
            }
        }
    }
}
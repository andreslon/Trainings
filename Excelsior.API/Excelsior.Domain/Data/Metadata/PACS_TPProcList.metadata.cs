using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Configuration;

//using EyeKor.Helpers;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_TPProcList.PACS_TPProcListMetadata))]
    public partial class PACS_TPProcList
    {
        internal sealed class PACS_TPProcListMetadata
        {
            [Association("TPProcList_ImgProcedureList_Association", "ImgProcedureID", "ImgProcedureID")]
            
            public CERT_ImgProcedureList CERTImgProcedureList
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Procedure is required.")]
            public long? ImgProcedureID
            {
                get;
                set;
            }

            [Association("PACS_TPProcList_GRD_GradingTemplate_Association", "GTemplateID", "GTemplateID")]
            
            public GRD_GradingTemplate GRDGradingTemplate
            {
                get;
                set;
            }

            [Association("PACS_TPProcList_WF_Template_Association", "WFTemplateID", "WFTemplateID")]
            
            public WF_Template WFTemplate
            {
                get;
                set;
            }

            //[RegularExpressionCustom("^[0-9]+$", ErrorMessageResourceName = "ValidationErrorOnlyInt", ErrorMessageResourceType = typeof(ValidationErrorResources))]
            //[StringLength(3, MinimumLength = 1, ErrorMessageResourceName = "ValidationErrorBadPercentageLength", ErrorMessageResourceType = typeof(ValidationErrorResources))]
            //[IntegerValidator(MaxValue = 100, MinValue = 0)]
            [Range(0, 100, ErrorMessage = "Value should be between 0 .. 100")]
            public string PercentSeriesForReview
            {
                get;
                set;
            }

        }
    }
}
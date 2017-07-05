using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_GradingTemplate.GRD_GradingTemplateMetadata))]
    public partial class GRD_GradingTemplate
    {
        internal sealed class GRD_GradingTemplateMetadata
        {
            [Required(ErrorMessage = "Name is required.")]
            public string GTemplateName
            {
                get;
                set;
            }

            [Association("GRDGradingTemplate_PACSTrial_Association", "TrialID", "TrialID")]
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }

            //[Association("GRD_QuestionGroups_Association", "GTemplateID", "GTemplateID")]
            //
            //public IList<GRD_QuestionGroup> GRD_QuestionGroups
            //{
            //    get;
            //    set;
            //}

            //[Association("PACS_TPProcList_Association", "GTemplateID", "GTemplateID")]
            //
            //public IList<PACS_TPProcList> PACS_TPProcLists
            //{
            //    get;
            //    set;
            //}              
        }
    }
}
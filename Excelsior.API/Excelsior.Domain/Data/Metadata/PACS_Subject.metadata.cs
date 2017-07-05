using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_Subject.PACS_SubjectMetadata))]
    public partial class PACS_Subject
    {
        internal sealed class PACS_SubjectMetadata
        {
            [Required(ErrorMessage = "Site is required.")]
            public long? SiteID
            {
                get;
                set;
            }

            [Association("Site_Association", "SiteID", "SiteID")]
            
            public PACS_Site PACSSite
            {
                get;
                set;
            }

            [Association("SubjectGroup_Association", "SubjectGroupID", "SubjectGroupID")]
            
            public PACS_SubjectGroup PACSSubjectGroup
            {
                get;
                set;
            }

            [Association("SubjectCohort_Association", "SubjectCohortID", "SubjectCohortID")]
            
            public PACS_SubjectCohort PACSSubjectCohort
            {
                get;
                set;
            }
        }
    }
}
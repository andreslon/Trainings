using System.ComponentModel.DataAnnotations;



namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_SubjectCohort.PACS_SubjectCohortMetadata))]
    public partial class PACS_SubjectCohort
    {
        internal sealed class PACS_SubjectCohortMetadata
        {
            [Required(ErrorMessage = "Name is required.")]
            public string CohortName
            {
                get;
                set;
            }
        }
    }
}
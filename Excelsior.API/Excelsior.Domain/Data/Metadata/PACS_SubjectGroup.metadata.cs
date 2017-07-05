using System.ComponentModel.DataAnnotations;



namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_SubjectGroup.PACS_SubjectGroupMetadata))]
    public partial class PACS_SubjectGroup
    {
        internal sealed class PACS_SubjectGroupMetadata
        {
            [Required(ErrorMessage = "Name is required.")]
            public string GroupName
            {
                get;
                set;
            }
        }
    }
}
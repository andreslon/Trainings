using System.ComponentModel.DataAnnotations;



namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_TimePoint.PACS_TimePointMetadata))]
    public partial class PACS_TimePoint
    {
        internal sealed class PACS_TimePointMetadata
        {
            [Association("Subject_Association", "SubjectID", "SubjectID")]
            
            public PACS_Subject PACSSubject
            {
                get;
                set;
            }

            [Association("TimePointsList_Association", "TimePointsListID", "TimePointsListID")]
            
            public PACS_TimePointsList PACSTimePointsList
            {
                get;
                set;
            }

            [Required(ErrorMessage="Timepoint is required")]
            public long? TimePointsListID
            {
                get;
                set;
            }
        }
    }
}
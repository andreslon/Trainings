using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_TimePointsList.PACS_TimePointsListMetadata))]
    public partial class PACS_TimePointsList
    {
        internal sealed class PACS_TimePointsListMetadata
        {
            [Required(ErrorMessage = "Name is required")]
            public string TimePointsDescription { get; set; }
        }
    }
}
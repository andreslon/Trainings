using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_SeriesGroup.PACS_SeriesGroupMetadata))]
    public partial class PACS_SeriesGroup
    {
        internal class PACS_SeriesGroupMetadata
        {
            [Association("Group_TimePoint_Association", "TimePointsID", "TimePointsID")]
            
            public PACS_TimePoint PACSTimePoint
            {
                get;
                set;
            }

            [Association("Group_Template_Association", "GTemplateID", "GTemplateID")]
            
            public GRD_GradingTemplate GRDGradingTemplate
            {
                get;
                set;
            }
        }
    }
}
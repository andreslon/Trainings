using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [XmlInclude(typeof(WF_Sequence))]
    [KnownType(typeof(WF_Sequence))]
    [MetadataTypeAttribute(typeof(PACS_Series.PACS_SeriesMetadata))]
    public partial class PACS_Series
    {
        internal class PACS_SeriesMetadata
        {
            //[Required(ErrorMessage = "Procedure is required.")]
            //public long? TPProcListID
            //{
            //    get;
            //    set;
            //}

            //[Required(ErrorMessage = "Timepoint is required.")]
            //public long? TimePointsID
            //{
            //    get;
            //    set;
            //}

            //[Required(ErrorMessage = "Technician is required.")]
            //public long? PhotographerID
            //{
            //    get;
            //    set;
            //}

            [Required(ErrorMessage = "Equipment is required.")]
            public long? EquipmentID
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Study date is required.")]
            public DateTime StudyDate
            {
                get;
                set;
            }

            [Association("Series_Photographer_Association", "PhotographerID", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }

            [Association("Series_Equipment_Association", "EquipmentID", "EquipmentID")]
            
            public CONTACT_Equipment CONTACTEquipment
            {
                get;
                set;
            }

            [Association("Series_TPProcList_Association", "TPProcListID", "TPProcID")]
            
            public PACS_TPProcList PACSTPProcList
            {
                get;
                set;
            }

            [Association("Series_TimePoint_Association", "TimePointsID", "TimePointsID")]
            
            public PACS_TimePoint PACSTimePoint
            {
                get;
                set;
            }

            [Association("Series_Group_Association", "SeriesGroupID", "SeriesGroupID")]
            
            public PACS_SeriesGroup PACSSeriesGroup
            {
                get;
                set;
            }

            [Association("Series_Queries_Association", "SeriesID", "SeriesID")]
            
            public IList<QRY_Query> QRY_Queries
            {
                get;
                set;
            }
        }
    }
}
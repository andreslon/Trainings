using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [XmlInclude(typeof(MEA_Area))]
    [XmlInclude(typeof(MEA_Distance))]
    [XmlInclude(typeof(MEA_ETDRSGrid))]
    [XmlInclude(typeof(MEA_Freehand))]
    [XmlInclude(typeof(MEA_OCTGrid))]
    [XmlInclude(typeof(MEA_OCTLayer))]
    [KnownType(typeof(MEA_Area))]
    [KnownType(typeof(MEA_Distance))]
    [KnownType(typeof(MEA_ETDRSGrid))]
    [KnownType(typeof(MEA_Freehand))]
    [KnownType(typeof(MEA_OCTGrid))]
    [KnownType(typeof(MEA_OCTLayer))]
    [KnownType(typeof(MEA_DeltaVolume))]
    [MetadataTypeAttribute(typeof(MEA_Measurement.MEA_MeasurementMetadata))]
    public partial class MEA_Measurement
    {
        internal class MEA_MeasurementMetadata
        {
            [Association("RawData_Association", "RawDataID", "RawDataID")]
            
            public PACS_RawDatum PACSRawDatum
            {
                get;
                set;
            }
            
            [Association("DicomFrame_Association", "DicomFrameID", "DicomFrameID")]
            
            public PACS_DicomFrame PACSDicomFrame
            {
                get;
                set;
            }

            [Association("MEAMeasurementType_Association", "MeasurementTypeID", "MeasurementTypeID")]
            
            public MEA_MeasurementType MEAMeasurementType
            {
                get;
                set;
            }

            [Association("GRDReport_Association", "GReportID", "GReportID")]
            
            public GRD_Report GRDReport
            {
                get;
                set;
            }
        }
    }
}
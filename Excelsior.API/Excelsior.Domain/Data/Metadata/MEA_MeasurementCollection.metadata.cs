using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(MEA_MeasurementCollection.MEA_MeasurementCollectionMetadata))]
    public partial class MEA_MeasurementCollection
    {
        internal class MEA_MeasurementCollectionMetadata
        {
            [Association("MEAMeasurements_Association", "Id", "MeasurementCollectionID")]
            
            public IList<MEA_Measurement> MEA_Measurements
            {
                get;
                set;
            }
        }
    }
}
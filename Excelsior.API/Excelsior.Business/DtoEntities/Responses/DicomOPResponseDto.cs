using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class DicomOPResponseDto
    {
        public long? RawDataID { get; set; }
        public double? PixelSpacingX { get; set; }
        public double? PixelSpacingY { get; set; }
        public string BolusTime { get; set; }
        public string AcquisitionTime { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
    }
}

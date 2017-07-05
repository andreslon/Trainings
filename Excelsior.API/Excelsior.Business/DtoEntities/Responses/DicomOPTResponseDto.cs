using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class DicomOPTResponseDto
    {
        public long? RawDataID { get; set; }
        public double? PixelSpacingX { get; set; }
        public double? PixelSpacingY { get; set; }
        public double? FrameSpacing { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public string ScanType { get; set; }
        public string RefImageCoveredArea { get; set; }
        public string RefDCMInstanceUID { get; set; }
        public long? RefRawDataID { get; set; }
    }
}

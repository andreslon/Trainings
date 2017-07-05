using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class FramesResponseDto
    {
        public long? DicomFrameID { get; set; }
        public long? RawDataID { get; set; }
        public string FrameImageLocation { get; set; }
        public int? FrameIndex { get; set; }
        public int? ImageHeight { get; set; }
        public int? ImageWidth { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsKeyFrame { get; set; }
        public string RefLineCoordinates { get; set; }
        public string RefLineType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class DicomWSIResponseDto
    {
        public long? RawDataID { get; set; }
        public double? PixelSpacingX { get; set; }
        public double? PixelSpacingY { get; set; }
        public long? WSIImageWidth { get; set; }
        public long? WSIImageHeight { get; set; }
        public int? TileSizeX { get; set; }
        public int? TileSizeY { get; set; }
        public short? TileOverlap { get; set; }
        public string TileFormat { get; set; }
    }
}

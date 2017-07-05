using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class DicomWSIBaseDto
    {
        public DicomWSIBaseDto()
            : this(null)
        {

        }
        public DicomWSIBaseDto(PACS_DicomWSI entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.RawDataID;
                PixelSpacingX = entity.PixelSpacingX;
                PixelSpacingY = entity.PixelSpacingY;
                WSIImageWidth = entity.WSIImageWidth;
                WSIImageHeight = entity.WSIImageHeight;
                TileSizeX = entity.TileSizeX;
                TileSizeY = entity.TileSizeY;
                TileOverlap = entity.TileOverlap;
                TileFormat = entity.TileFormat;
            }
        }
        public virtual PACS_DicomWSI ToEntity(PACS_DicomWSI entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_DicomWSI();
            }

            entity.RawDataID = Id.GetValueOrDefault();
            entity.PixelSpacingX = PixelSpacingX;
            entity.PixelSpacingY = PixelSpacingY;
            entity.WSIImageWidth = WSIImageWidth;
            entity.WSIImageHeight = WSIImageHeight;
            entity.TileSizeX = TileSizeX;
            entity.TileSizeY = TileSizeY;
            entity.TileOverlap = TileOverlap;
            entity.TileFormat = TileFormat;

            return entity;
        }

        public long? Id { get; set; }
        [Range(0, double.MaxValue)]
        public double? PixelSpacingX { get; set; }
        [Range(0, double.MaxValue)]
        public double? PixelSpacingY { get; set; }
        [Range(0, long.MaxValue)]
        public long? WSIImageWidth { get; set; }
        [Range(0, long.MaxValue)]
        public long? WSIImageHeight { get; set; }
        [Range(0, int.MaxValue)]
        public int? TileSizeX { get; set; }
        [Range(0, int.MaxValue)]
        public int? TileSizeY { get; set; }
        [Range(0, short.MaxValue)]
        public short? TileOverlap { get; set; }
        [StringLength(10)]
        public string TileFormat { get; set; }
    }
}

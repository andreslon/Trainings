using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class DicomOPTBaseDto
    {
        public DicomOPTBaseDto()
            : this(null)
        {

        }
        public DicomOPTBaseDto(PACS_DicomOPT entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.RawDataID;
                PixelSpacingX = entity.PixelSpacingX;
                PixelSpacingY = entity.PixelSpacingY;
                FrameSpacing = entity.FrameSpacing;
                ScanType = entity.ScanType;
                ImageHeight = entity.ImageHeight;
                ImageWidth = entity.ImageWidth;
                RefImageCoveredArea = entity.RefImageCoveredArea;
                RefDCMInstanceUId = entity.RefDCMInstanceUID;
                RefMediaId = entity.RefRawDataID;
            }
        }
        public virtual PACS_DicomOPT ToEntity(PACS_DicomOPT entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_DicomOPT();
            }

            entity.RawDataID = Id.GetValueOrDefault();
            entity.PixelSpacingX = PixelSpacingX;
            entity.PixelSpacingY = PixelSpacingY;
            entity.FrameSpacing = FrameSpacing;
            entity.ScanType = ScanType;
            entity.ImageHeight = ImageHeight;
            entity.ImageWidth = ImageWidth;
            entity.RefImageCoveredArea = RefImageCoveredArea;
            entity.RefDCMInstanceUID = RefDCMInstanceUId;
            entity.RefRawDataID = RefMediaId;

            return entity;
        }

        public long? Id { get; set; }
        [Range(0, double.MaxValue)]
        public double? PixelSpacingX { get; set; }
        [Range(0, double.MaxValue)]
        public double? PixelSpacingY { get; set; }
        [Range(0, double.MaxValue)]
        public double? FrameSpacing { get; set; }
        [Range(0, int.MaxValue)]
        public int? ImageWidth { get; set; }
        [Range(0, int.MaxValue)]
        public int? ImageHeight { get; set; }
        [StringLength(32)]
        public string ScanType { get; set; }
        public string RefImageCoveredArea { get; set; }
        [StringLength(64)]
        public string RefDCMInstanceUId { get; set; }
        [Range(0, long.MaxValue)]
        public long? RefMediaId { get; set; }
    }
}

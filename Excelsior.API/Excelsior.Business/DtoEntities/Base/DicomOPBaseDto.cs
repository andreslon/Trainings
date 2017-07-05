using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class DicomOPBaseDto  
    {
        public DicomOPBaseDto()
            : this(null)
        {

        }
        public DicomOPBaseDto(PACS_DicomOP entity, object sender = null) 
        { 
            if (entity != null)
            {
                Id = entity.RawDataID;
                PixelSpacingX = entity.PixelSpacingX;
                PixelSpacingY = entity.PixelSpacingY;
                BolusTime = entity.BolusTime;
                AcquisitionTime = entity.AcquisitionTime;
                ImageHeight = entity.ImageHeight;
                ImageWidth = entity.ImageWidth;
            }
        }
        public virtual PACS_DicomOP ToEntity(PACS_DicomOP entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_DicomOP();
            }
            entity.RawDataID = Id.GetValueOrDefault();
            entity.PixelSpacingX = PixelSpacingX;
            entity.PixelSpacingY = PixelSpacingY;
            entity.BolusTime = BolusTime;
            entity.AcquisitionTime = AcquisitionTime;
            entity.ImageHeight = ImageHeight;
            entity.ImageWidth = ImageWidth;
            return entity;
        }       

        public long? Id { get; set; }
        [Range(0, double.MaxValue)]
        public double? PixelSpacingX { get; set; }
        [Range(0, double.MaxValue)]
        public double? PixelSpacingY { get; set; }
        [StringLength(50)]
        public string BolusTime { get; set; }
        [StringLength(50)]
        public string AcquisitionTime { get; set; }
        [Range(0, int.MaxValue)]
        public int? ImageWidth { get; set; }
        [Range(0, int.MaxValue)]
        public int? ImageHeight { get; set; }        
    }
}

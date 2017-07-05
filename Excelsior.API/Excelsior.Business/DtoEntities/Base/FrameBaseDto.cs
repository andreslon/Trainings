using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Base
{
    public class FrameBaseDto
    {
        public FrameBaseDto()
            : this(null)
        {

        }
        public FrameBaseDto(PACS_DicomFrame entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.DicomFrameID;
                MediaId = entity.RawDataID;
                ImageWidth = entity.ImageWidth;
                ImageHeight = entity.ImageHeight;
                Index = entity.FrameIndex;
                ImageLocation = entity.FrameImageLocation;
                IsActive = entity.IsActive;
                RefLineType = entity.RefLineType;
                RefLineCoordinates = entity.RefLineCoordinates;
                IsKeyFrame = entity.IsKeyFrame;
            }
        }
        public virtual PACS_DicomFrame ToEntity(PACS_DicomFrame entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new PACS_DicomFrame();
            }
            entity.DicomFrameID = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["mediaid"])
                entity.RawDataID = MediaId;
                if (fieldvalidation["imagewidth"])
                entity.ImageWidth = ImageWidth;
                if (fieldvalidation["imageheight"])
                entity.ImageHeight = ImageHeight;
                if (fieldvalidation["index"])
                entity.FrameIndex = Index;
                if (fieldvalidation["imagelocation"])
                entity.FrameImageLocation = ImageLocation;
                if (fieldvalidation["isactive"])
                entity.IsActive = IsActive;
                if (fieldvalidation["reflinetype"])
                entity.RefLineType = RefLineType;
                if (fieldvalidation["reflinecoordinates"])
                entity.RefLineCoordinates = RefLineCoordinates;
                if (fieldvalidation["iskeyframe"])
                entity.IsKeyFrame = IsKeyFrame;
            }



            return entity;
        }

        public long Id { get; set; }
        public long? MediaId { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public int? Index { get; set; }
        public string ImageLocation { get; set; }
        public bool IsActive { get; set; }
        public string RefLineType { get; set; }
        public string RefLineCoordinates { get; set; }
        public bool IsKeyFrame { get; set; }
    }
}

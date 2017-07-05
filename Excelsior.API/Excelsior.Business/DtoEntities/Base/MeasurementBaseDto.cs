using Excelsior.Business.DtoEntities.Full;
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
    public class MeasurementBaseDto
    {
        public MeasurementBaseDto()
            : this(null)
        {

        }
        public MeasurementBaseDto(MEA_Measurement entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.MeasurementID;
                ReportId = entity.GReportID;
                MeasurementTypeId = entity.MeasurementTypeID;
                FrameId = entity.DicomFrameID;
                MediaId = entity.RawDataID;
                if(MediaId == null)
                {
                    if(entity.PACSDicomFrame != null)
                    {
                        MediaId = entity.PACSDicomFrame.RawDataID;
                    }
                }
                if (SeriesId == null)
                {
                    if (entity.PACSDicomFrame != null)
                    {
                        SeriesId = entity.PACSDicomFrame.PACSRawDatum.SeriesID;
                    }
                    else if(entity.PACSRawDatum != null)
                    {
                        SeriesId = entity.PACSRawDatum.SeriesID;
                    }
                }

                if (!(sender is MeasurementTypeBaseDto) && entity.MEAMeasurementType != null)
                {
                    MeasurementType = new MeasurementTypeFullDto(entity.MEAMeasurementType);
                }

                switch (MeasurementType.Name)
                {
                    case "Freehand":
                        Freehand = new MeaFreehandBaseDto(entity as MEA_Freehand);
                        break;
                    case "Distance":
                        Distance = new MeaDistanceBaseDto(entity as MEA_Distance);
                        break;
                    case "Area":
                        Area = new MeaAreaBaseDto(entity as MEA_Area);
                        break;
                    case "ETDRSGrid":
                        ETDRSGrid = new MeaETDRSGridBaseDto(entity as MEA_ETDRSGrid);
                        break;
                    case "OCTGrid":
                        OCTGrid = new MeaOCTGridBaseDto(entity as MEA_OCTGrid);
                        break;
                    case "DeltaVolume":
                        DeltaVolume = new MeaDeltaVolumeBaseDto(entity as MEA_DeltaVolume);
                        break;
                }

            }
        }
        public virtual MEA_Measurement ToEntity(MEA_Measurement entity = null, string fields = null)
        {
            if (MeasurementType == null)
                return null;
            if (entity == null)
            {
                switch(MeasurementType.Name)
                {
                    case "Freehand":
                        entity = new MEA_Freehand();
                        entity = Freehand.ToEntity(entity as MEA_Freehand);
                        break;
                    case "Distance":
                        entity = new MEA_Distance();
                        entity = Distance.ToEntity(entity as MEA_Distance);
                        break;
                    case "Area":
                        entity = new MEA_Area();
                        entity = Area.ToEntity(entity as MEA_Area);
                        break;
                    case "ETDRSGrid":
                        entity = new MEA_ETDRSGrid();
                        entity = ETDRSGrid.ToEntity(entity as MEA_ETDRSGrid);
                        break;
                    case "OCTGrid":
                        entity = new MEA_OCTGrid();
                        entity = OCTGrid.ToEntity(entity as MEA_OCTGrid);
                        break;
                    case "DeltaVolume":
                        entity = new MEA_DeltaVolume();
                        entity = DeltaVolume.ToEntity(entity as MEA_DeltaVolume);
                        break;
                }
            }
            entity.MeasurementID = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["reportid"])
                    entity.GReportID = ReportId;
                if (fieldvalidation["measurementtypeId"])
                    entity.MeasurementTypeID = MeasurementTypeId;
                if (fieldvalidation["frameid"])
                    entity.DicomFrameID = FrameId;
                if (fieldvalidation["mediaid"])
                    entity.RawDataID = MediaId;
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? ReportId { get; set; }
        [Range(0, long.MaxValue)]
        public long? MeasurementTypeId { get; set; }
        [Range(0, long.MaxValue)]
        public long? FrameId { get; set; }
        [Range(0, long.MaxValue)]
        public long? MediaId { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }

        public MeasurementTypeFullDto MeasurementType { get; set; }

        public MeaAreaBaseDto Area { get; set; }
        public MeaDistanceBaseDto Distance { get; set; }
        public MeaFreehandBaseDto Freehand { get; set; }
        public MeaETDRSGridBaseDto ETDRSGrid { get; set; }
        public MeaOCTGridBaseDto OCTGrid { get; set; }
        public MeaDeltaVolumeBaseDto DeltaVolume { get; set; }
    }
}
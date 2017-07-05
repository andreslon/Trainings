﻿using Excelsior.Business.DtoEntities.Full;
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
    public class MeaDeltaVolumeBaseDto
    {
        public MeaDeltaVolumeBaseDto()
            : this(null)
        {

        }
        public MeaDeltaVolumeBaseDto(MEA_DeltaVolume entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.MeasurementID;
                AreaSize = entity.AreaSizeMm2;
                DistanceToFovea = entity.DistanceToFoveaMm;
                Perimeter = entity.PerimeterMm;
                AreaLabel = entity.AreaLabel;
                Xml = entity.MeasurementXML;
                Volume = entity.Volume;
                DeltaVolume = entity.DeltaVolume;
                GLD = entity.GLD;
            }
        }
        public virtual MEA_DeltaVolume ToEntity(MEA_DeltaVolume entity = null, string fields = null)
        {
            if (entity != null)
            {
                using (var fieldvalidation = new FieldValidation(fields))
                {
                    if (fieldvalidation["areasize"])
                        entity.AreaSizeMm2 = AreaSize;
                    if (fieldvalidation["distancetofovea"])
                        entity.DistanceToFoveaMm = DistanceToFovea;
                    if (fieldvalidation["perimeter"])
                        entity.PerimeterMm = Perimeter;
                    if (fieldvalidation["arealabel"])
                        entity.AreaLabel = AreaLabel;
                    if (fieldvalidation["xml"])
                        entity.MeasurementXML = Xml;
                    if (fieldvalidation["volume"])
                        entity.Volume = Volume;
                    if (fieldvalidation["deltavolume"])
                        entity.DeltaVolume = DeltaVolume;
                    if (fieldvalidation["gld"])
                        entity.GLD = GLD;
                }
            }
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        public double? AreaSize { get; set; }
        public double? DistanceToFovea { get; set; }
        public double? Perimeter { get; set; }
        [StringLength(50)]
        public string AreaLabel { get; set; }
        public string Xml { get; set; }
        public double? Volume { get; set; }
        public double? DeltaVolume { get; set; }
        public double? GLD { get; set; }
    }
}
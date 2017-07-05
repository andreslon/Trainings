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
    public class MeaOCTGridBaseDto
    {
        public MeaOCTGridBaseDto()
            : this(null)
        {

        }
        public MeaOCTGridBaseDto(MEA_OCTGrid entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.MeasurementID;
                OCTGridLabel = entity.OCTGridLabel;
                OCTGridLayer1 = entity.OCTGridLayer1;
                OCTGridLayer2 = entity.OCTGridLayer2;
                CenterLocationAscan = entity.CenterLocationAscan;
                CenterLocationFrame = entity.CenterLocationFrame;
                CenterPointThickness = entity.CenterPointThicknessMm;
                TotalVolume = entity.TotalVolumeMm3;
                Sector0Thick = entity.Sector0Thick;
                Sector1Thick = entity.Sector1Thick;
                Sector2Thick = entity.Sector2Thick;
                Sector3Thick = entity.Sector3Thick;
                Sector4Thick = entity.Sector4Thick;
                Sector5Thick = entity.Sector5Thick;
                Sector6Thick = entity.Sector6Thick;
                Sector7Thick = entity.Sector7Thick;
                Sector8Thick = entity.Sector8Thick;
                Sector0Vol = entity.Sector0Vol;
                Sector1Vol = entity.Sector1Vol;
                Sector2Vol = entity.Sector2Vol;
                Sector3Vol = entity.Sector3Vol;
                Sector4Vol = entity.Sector4Vol;
                Sector5Vol = entity.Sector5Vol;
                Sector6Vol = entity.Sector6Vol;
                Sector7Vol = entity.Sector7Vol;
                Sector8Vol = entity.Sector8Vol;
            }
        }
        public virtual MEA_OCTGrid ToEntity(MEA_OCTGrid entity = null, string fields = null)
        {
            if (entity != null)
            {
                using (var fieldvalidation = new FieldValidation(fields))
                {
                    if (fieldvalidation["octgridlabel"])
                        entity.OCTGridLabel = OCTGridLabel;
                    if (fieldvalidation["octgridlayer1"])
                        entity.OCTGridLayer1 = OCTGridLayer1;
                    if (fieldvalidation["octgridlayer2"])
                        entity.OCTGridLayer2 = OCTGridLayer2;
                    if (fieldvalidation["centerlocationascan"])
                        entity.CenterLocationAscan = CenterLocationAscan;
                    if (fieldvalidation["centerlocationframe"])
                        entity.CenterLocationFrame = CenterLocationFrame;
                    if (fieldvalidation["centerpointthickness"])
                        entity.CenterPointThicknessMm = CenterPointThickness;
                    if (fieldvalidation["totalvolume"])
                        entity.TotalVolumeMm3 = TotalVolume;
                    if (fieldvalidation["sector0thick"])
                        entity.Sector0Thick = Sector0Thick;
                    if (fieldvalidation["sector1thick"])
                        entity.Sector1Thick = Sector1Thick;
                    if (fieldvalidation["sector2thick"])
                        entity.Sector2Thick = Sector2Thick;
                    if (fieldvalidation["sector3thick"])
                        entity.Sector3Thick = Sector3Thick;
                    if (fieldvalidation["sector4thick"])
                        entity.Sector4Thick = Sector4Thick;
                    if (fieldvalidation["sector5thick"])
                        entity.Sector5Thick = Sector5Thick;
                    if (fieldvalidation["sector6thick"])
                        entity.Sector6Thick = Sector6Thick;
                    if (fieldvalidation["sector7thick"])
                        entity.Sector7Thick = Sector7Thick;
                    if (fieldvalidation["sector8thick"])
                        entity.Sector8Thick = Sector8Thick;
                    if (fieldvalidation["sector0vol"])
                        entity.Sector0Vol = Sector0Vol;
                    if (fieldvalidation["sector1vol"])
                        entity.Sector1Vol = Sector1Vol;
                    if (fieldvalidation["sector2vol"])
                        entity.Sector2Vol = Sector2Vol;
                    if (fieldvalidation["sector3vol"])
                        entity.Sector3Vol = Sector3Vol;
                    if (fieldvalidation["sector4vol"])
                        entity.Sector4Vol = Sector4Vol;
                    if (fieldvalidation["sector5vol"])
                        entity.Sector5Vol = Sector5Vol;
                    if (fieldvalidation["sector6vol"])
                        entity.Sector6Vol = Sector6Vol;
                    if (fieldvalidation["sector7vol"])
                        entity.Sector7Vol = Sector7Vol;
                    if (fieldvalidation["sector8vol"])
                        entity.Sector8Vol = Sector8Vol;
                }
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        public string OCTGridLabel { get; set; }
        public string OCTGridLayer1 { get; set; }
        public string OCTGridLayer2 { get; set; }
        public double? CenterLocationAscan { get; set; }
        public double? CenterLocationFrame { get; set; }
        public double? CenterPointThickness { get; set; }
        public double? TotalVolume { get; set; }
        public double? Sector0Thick { get; set; }
        public double? Sector1Thick { get; set; }
        public double? Sector2Thick { get; set; }
        public double? Sector3Thick { get; set; }
        public double? Sector4Thick { get; set; }
        public double? Sector5Thick { get; set; }
        public double? Sector6Thick { get; set; }
        public double? Sector7Thick { get; set; }
        public double? Sector8Thick { get; set; }
        public double? Sector0Vol { get; set; }
        public double? Sector1Vol { get; set; }
        public double? Sector2Vol { get; set; }
        public double? Sector3Vol { get; set; }
        public double? Sector4Vol { get; set; }
        public double? Sector5Vol { get; set; }
        public double? Sector6Vol { get; set; }
        public double? Sector7Vol { get; set; }
        public double? Sector8Vol { get; set; }
    }
}

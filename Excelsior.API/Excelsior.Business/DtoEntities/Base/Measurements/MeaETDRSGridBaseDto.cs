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
    public class MeaETDRSGridBaseDto
    {
        public MeaETDRSGridBaseDto()
            : this(null)
        {

        }
        public MeaETDRSGridBaseDto(MEA_ETDRSGrid entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.MeasurementID;
                FoveaLocationX = entity.FoveaLocationX;
                FoveaLocationY = entity.FoveaLocationY;
                ONHLocationX = entity.ONHLocationX;
                ONHLocationY = entity.ONHLocationY;
                Sector0 = entity.Sector0;
                Sector1 = entity.Sector1;
                Sector2 = entity.Sector2;
                Sector3 = entity.Sector3;
                Sector4 = entity.Sector4;
                Sector5 = entity.Sector5;
                Sector6 = entity.Sector6;
                Sector7 = entity.Sector7;
                Sector8 = entity.Sector8;
            }
        }
        public virtual MEA_ETDRSGrid ToEntity(MEA_ETDRSGrid entity = null, string fields = null)
        {
            if (entity != null)
            {
                using (var fieldvalidation = new FieldValidation(fields))
                {
                    if (fieldvalidation["foveaLocationx"])
                        entity.FoveaLocationX = FoveaLocationX;
                    if (fieldvalidation["foveaLocationy"])
                        entity.FoveaLocationY = FoveaLocationY;
                    if (fieldvalidation["onhlocationx"])
                        entity.ONHLocationX = ONHLocationX;
                    if (fieldvalidation["onhlocationy"])
                        entity.ONHLocationY = ONHLocationY;
                    if (fieldvalidation["sector0"])
                        entity.Sector0 = Sector0;
                    if (fieldvalidation["sector1"])
                        entity.Sector1 = Sector1;
                    if (fieldvalidation["sector2"])
                        entity.Sector2 = Sector2;
                    if (fieldvalidation["sector3"])
                        entity.Sector3 = Sector3;
                    if (fieldvalidation["sector4"])
                        entity.Sector4 = Sector4;
                    if (fieldvalidation["sector5"])
                        entity.Sector5 = Sector5;
                    if (fieldvalidation["sector6"])
                        entity.Sector6 = Sector6;
                    if (fieldvalidation["sector7"])
                        entity.Sector7 = Sector7;
                    if (fieldvalidation["sector8"])
                        entity.Sector8 = Sector8;
                }
            }
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        public double? FoveaLocationX { get; set; }
        public double? FoveaLocationY { get; set; }
        public double? ONHLocationX { get; set; }
        public double? ONHLocationY { get; set; }
        public double? Sector0 { get; set; }
        public double? Sector1 { get; set; }
        public double? Sector2 { get; set; }
        public double? Sector3 { get; set; }
        public double? Sector4 { get; set; }
        public double? Sector5 { get; set; }
        public double? Sector6 { get; set; }
        public double? Sector7 { get; set; }
        public double? Sector8 { get; set; }
    }
}

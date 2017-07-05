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
    public class MeaDistanceBaseDto
    {
        public MeaDistanceBaseDto()
            : this(null)
        {

        }
        public MeaDistanceBaseDto(MEA_Distance entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.MeasurementID;
                Distance = entity.DistanceMm;
                StartX = entity.StartX;
                StartY = entity.StartY;
                EndX = entity.EndX;
                EndY = entity.EndY;
            }
        }
        public virtual MEA_Distance ToEntity(MEA_Distance entity = null, string fields = null)
        {
            if (entity != null)
            {
                using (var fieldvalidation = new FieldValidation(fields))
                {
                    if (fieldvalidation["Distance"])
                        entity.DistanceMm = Distance;
                    if (fieldvalidation["StartX"])
                        entity.StartX = StartX;
                    if (fieldvalidation["StartY"])
                        entity.StartY = StartY;
                    if (fieldvalidation["EndX"])
                        entity.EndX = EndX;
                    if (fieldvalidation["EndY"])
                        entity.EndY = EndY;
                }
            }
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        public double? Distance { get; set; }
        public double? StartX { get; set; }
        public double? StartY { get; set; }
        public double? EndX { get; set; }
        public double? EndY { get; set; }
    }
}

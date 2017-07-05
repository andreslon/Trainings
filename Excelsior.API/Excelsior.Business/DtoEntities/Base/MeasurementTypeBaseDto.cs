using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Base
{
    public class MeasurementTypeBaseDto
    {
        public MeasurementTypeBaseDto()
            : this(null)
        {

        }
        public MeasurementTypeBaseDto(MEA_MeasurementType entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.MeasurementTypeID;
                Name = entity.MeasurementType;
            }
        }
        public virtual MEA_MeasurementType ToEntity(MEA_MeasurementType entity = null)
        {
            if (entity == null)
            {
                entity = new MEA_MeasurementType();
            }
            entity.MeasurementTypeID = Id;
            entity.MeasurementType = Name;


            return entity;
        }
        public long Id { get; set; }
        public string Name { get; set; }

    }
}

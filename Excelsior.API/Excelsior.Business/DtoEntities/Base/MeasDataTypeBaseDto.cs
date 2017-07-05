using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Base
{
    public class MeasDataTypeBaseDto
    {
        public MeasDataTypeBaseDto()
            : this(null)
        {

        }
        public MeasDataTypeBaseDto(MEA_MeasDataType entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.MeasDataTypeID;
                DataTypeId = entity.DataTypeID;
                MeasurementTypeId = entity.MeasurementTypeID;
            }
        }
        public virtual MEA_MeasDataType ToEntity(MEA_MeasDataType entity = null)
        {
            if (entity == null)
            {
                entity = new MEA_MeasDataType();
            }
            entity.MeasDataTypeID = Id;
            entity.DataTypeID = DataTypeId;
            entity.MeasurementTypeID = MeasurementTypeId;

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        public long? DataTypeId { get; set; }
        public long? MeasurementTypeId { get; set; }
    }
}

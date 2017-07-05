using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Full
{
    public class MeasDataTypeFullDto: MeasDataTypeBaseDto
    {
        public MeasDataTypeFullDto()
            : this(null)
        {
          
        }
        public MeasDataTypeFullDto(MEA_MeasDataType entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is MediaTypeBaseDto) && entity.PACSDataType != null)
                {
                    DataType = new MediaTypeFullDto(entity.PACSDataType);
                }
                if (!(sender is MeasurementTypeBaseDto) && entity.MEAMeasurementType != null)
                {
                    MeasurementType = new MeasurementTypeFullDto(entity.MEAMeasurementType);
                }
            }
        }
        public override MEA_MeasDataType ToEntity(MEA_MeasDataType entity = null)
        {
            entity = base.ToEntity(entity);

            entity.PACSDataType = DataType.ToEntity(entity.PACSDataType);
            entity.MEAMeasurementType = MeasurementType.ToEntity(entity.MEAMeasurementType);

            return entity;
        }
        public MediaTypeFullDto DataType { get; set; }
        public MeasurementTypeFullDto MeasurementType { get; set; }
    }
}

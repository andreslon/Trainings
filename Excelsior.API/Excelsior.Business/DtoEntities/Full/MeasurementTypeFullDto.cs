using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Full
{
    public class MeasurementTypeFullDto : MeasurementTypeBaseDto
    {
        public MeasurementTypeFullDto()
            : this(null)
        {
        }
        public MeasurementTypeFullDto(MEA_MeasurementType entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override MEA_MeasurementType ToEntity(MEA_MeasurementType entity = null)
        {
            entity = base.ToEntity(entity);
            return entity;
        }
    }
}

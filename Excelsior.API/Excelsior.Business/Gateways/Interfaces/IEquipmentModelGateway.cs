using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.Gateways.Interfaces
{
    public interface IEquipmentModelGateway : IBaseGateway<EquipmentModelFullDto, EquipmentModelBaseDto, EquipmentModelRequestDto>
    {
    }
}

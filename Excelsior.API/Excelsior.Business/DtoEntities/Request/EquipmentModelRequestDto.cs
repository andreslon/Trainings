using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Request
{
    public class EquipmentModelRequestDto : BaseRequestDto
    {
        public long Id { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerModel { get; set; }
        public string EquipmentType { get; set; }
    }
}

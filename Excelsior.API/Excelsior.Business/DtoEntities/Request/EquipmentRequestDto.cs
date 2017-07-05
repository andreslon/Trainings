using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Request
{
    public class EquipmentRequestDto : BaseRequestDto
    {
        public long? Id { get; set; }
        public string StationName { get; set; }
        public string SoftwareVersion { get; set; }
        public string SeconarySerialNum { get; set; }
        public string OtherSerialNum { get; set; }
        public string Notes { get; set; }
        public string MainSerialNum { get; set; }
        public DateTime? LastCalibrationTime { get; set; }
        public DateTime? LastCalibrationDate { get; set; }
        public bool? IsValidated { get; set; }
        public bool? IsActive { get; set; }
        public string FirmwareVersion { get; set; }
        public long? EquipmentModelId { get; set; }
        public long? AffiliationId { get; set; }
    }
}

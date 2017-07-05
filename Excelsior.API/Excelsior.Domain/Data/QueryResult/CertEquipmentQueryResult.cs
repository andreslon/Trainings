using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain
{
    public class CertEquipmentQueryResult
    {
        public CERT_Equipment certequipment { get; set; }
        public CERT_ImgProcedureList procedure { get; set; }
        public CONTACT_Equipment equipment { get; set; }
        public CONTACT_EquipmentModel emodel { get; set; }
        public CONTACT_Affiliation affiliation { get; set; }
        public CONTACT_Country country { get; set; }
        public CONTACT_User certifiedby { get; set; }
        public CONTACT_User assignedto { get; set; }
        public PACS_Trial trial { get; set; }
        public int prevcert { get; set; }
    }
}

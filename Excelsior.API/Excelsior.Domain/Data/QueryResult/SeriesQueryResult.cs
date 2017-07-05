using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain
{
    public class SeriesQueryResult
    {
        public WF_Sequence series { get; set; }
        public PACS_TPProcList schedule { get; set; }
        public CERT_ImgProcedureList procedure { get; set; }
        public PACS_TimePointsList timepoint { get; set; }
        public PACS_TimePoint visit  { get; set; }
        public PACS_Subject subject { get; set; }
        public PACS_Site site { get; set; }
        public CONTACT_Affiliation affiliation  { get; set; }
        public CONTACT_Country country { get; set; }
        public CONTACT_Equipment equipment { get; set; }
        public CONTACT_EquipmentModel emodel { get; set; }
        public CONTACT_User tech { get; set; }
        public CONTACT_User assignedto { get; set; }
        public PACS_SubjectCohort scohort { get; set; }
        public PACS_SubjectGroup sgroup { get; set; }
        public WF_TempStep wftempstep { get; set; }
        public WF_StepList wfstep { get; set; }
    }
}

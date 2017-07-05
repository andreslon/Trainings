using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class TrialsResponseDto
    {
        public long? TrialID { get; set; }
        public string TrialAlias { get; set; }
        public DateTime? TrialEndDate { get; set; }
        public string TrialName { get; set; }
        public DateTime? TrialStartDate { get; set; }
        public string AnimalSpeciesDisplayName { get; set; }
        public long? TotalSubjects { get; set; }
        public string PrimaryDrugs { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? TrialLockedDate { get; set; }
    }
}

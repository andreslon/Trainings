using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class SubjectsResponseDto
    {
        public long? SubjectID { get; set; }
        public long? SiteID { get; set; }
        public string RandomizedSubjectID { get; set; }
        public string AlternativeRandomizedSubjectID { get; set; }
        public string NameCode { get; set; }
        public string Laterality { get; set; }
        public int? BirthYear { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsValidated { get; set; }
        public bool? IsSubjectRejected { get; set; }
        public bool? IsTestingSubject { get; set; }
        public long? SubjectGroupID { get; set; }
        public long? SubjectCohortID { get; set; }
        public DateTime? SubjectEnrollmentDate { get; set; }
        public string Gender { get; set; }
        public bool? IsDismissed { get; set; }

       public SitesResponseDto Site { get; set; }
      
    }
}

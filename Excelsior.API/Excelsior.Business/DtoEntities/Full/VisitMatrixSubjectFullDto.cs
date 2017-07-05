using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{

    public class VisitMatrixSubjectFullDto : SubjectBaseDto
    {
        public VisitMatrixSubjectFullDto()
            : this(null)
        {
            TimePoints = new List<VisitMatrixTimePointFullDto>();
            Procedures = new List<VisitMatrixProcedureFullDto>();
        }
        public VisitMatrixSubjectFullDto(PACS_Subject entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is SubjectGroupBaseDto) && entity.PACSSubjectGroup != null)
                {
                    Group = new SubjectGroupFullDto(entity.PACSSubjectGroup, this);
                }
                if (!(sender is SubjectCohortBaseDto) && entity.PACSSubjectCohort != null)
                {
                    Cohort = new SubjectCohortFullDto(entity.PACSSubjectCohort, this);
                }
                
            }
        }
        public override PACS_Subject ToEntity(PACS_Subject entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            if (Group != null)
            {
                entity.PACSSubjectGroup = Group.ToEntity(entity.PACSSubjectGroup);
            }
            if (Cohort != null)
            {
                entity.PACSSubjectCohort = Cohort.ToEntity(entity.PACSSubjectCohort);
            }
            return entity;
        }

        public List<VisitMatrixTimePointFullDto> TimePoints { get; set; }

        public List<VisitMatrixProcedureFullDto> Procedures { get; set; }

        public SubjectGroupFullDto Group { get; set; }

        public SubjectCohortFullDto Cohort { get; set; }
    }
}

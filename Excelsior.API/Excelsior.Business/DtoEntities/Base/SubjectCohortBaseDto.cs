using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class SubjectCohortBaseDto
    {
        public SubjectCohortBaseDto()
            : this(null)
        {
        }
        public SubjectCohortBaseDto(PACS_SubjectCohort entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.SubjectCohortID;
                Name = entity.CohortName;
                IsActive = entity.IsActive;
            }
        }
        public virtual PACS_SubjectCohort ToEntity(PACS_SubjectCohort entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_SubjectCohort();
            }

            entity.SubjectCohortID = Id.GetValueOrDefault();
            entity.CohortName = Name;
            entity.IsActive = IsActive.GetValueOrDefault(true); 

            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}

using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class SubjectGroupBaseDto
    {
        public SubjectGroupBaseDto()
            : this(null)
        {
        }
        public SubjectGroupBaseDto(PACS_SubjectGroup entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.SubjectGroupID;
                Name = entity.GroupName;
                Description = entity.GroupDescription;
                DosingInfo = entity.DosingInfo;
                TreatmentEye = entity.TreatmentEye;
                NumberOfSubjects = entity.NumberofSubjects;
                IsActive = entity.IsActive;
            }
        }
        public virtual PACS_SubjectGroup ToEntity(PACS_SubjectGroup entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_SubjectGroup();
            }

            entity.SubjectGroupID = Id.GetValueOrDefault();
           entity.GroupName = Name;
            entity.GroupDescription = Description;
            entity.DosingInfo = DosingInfo;
            entity.TreatmentEye = TreatmentEye;
            entity.NumberofSubjects = NumberOfSubjects;
            entity.IsActive = IsActive.GetValueOrDefault(true); 
            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string DosingInfo { get; set; }
        [StringLength(10)]
        public string TreatmentEye { get; set; }
        public string NumberOfSubjects { get; set; }
        public bool? IsActive { get; set; }
    }
}

using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class StudyBaseDto
    {
        public StudyBaseDto()
            : this(null)
        {

        }
        public StudyBaseDto(PACS_Trial entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.TrialID;
                Alias = entity.TrialAlias;
                EndDate = entity.TrialEndDate;
                Name = entity.TrialName;
                StartDate = entity.TrialStartDate;
                PrimaryDrugs = entity.PrimaryDrugs;
                IsActive = entity.IsActive;
                IsLocked = entity.IsLocked;
                LockedDate = entity.TrialLockedDate;
                AnimalSpeciesId = entity.AnimalSpeciesID;
                ImpressionId = entity.ImpressionID;

                if (!(sender is AnimalSpeciesBaseDto) && entity.CFGAnimalSpecy != null)
                {
                    AnimalSpecies = new AnimalSpeciesFullDto(entity.CFGAnimalSpecy, this);
                }

                if (!(sender is ImpressionBaseDto) && entity.GRDImpression != null)
                {
                    Impression = new ImpressionFullDto(entity.GRDImpression, this);
                }
            }
        }
        public virtual PACS_Trial ToEntity(PACS_Trial entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new PACS_Trial();
            }

            entity.TrialID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.TrialName = Name;
                if (fieldvalidation["alias"])
                    entity.TrialAlias = Alias;
                if (fieldvalidation["primarydrugs"])
                    entity.PrimaryDrugs = PrimaryDrugs;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["islocked"])
                    entity.IsLocked = IsLocked.GetValueOrDefault();
                if (fieldvalidation["lockeddate"])
                    entity.TrialLockedDate = LockedDate;
                if (fieldvalidation["enddate"])
                    entity.TrialEndDate = EndDate;
                if (fieldvalidation["startdate"])
                    entity.TrialStartDate = StartDate;
                if (fieldvalidation["animalspeciesid"])
                    entity.AnimalSpeciesID = AnimalSpeciesId;
                if (fieldvalidation["impressionid"])
                    entity.ImpressionID = ImpressionId;
            }

            return entity;
        }

        public long? Id { get; set; }
        [StringLength(256)]
        [Required]
        public string Name { get; set; }
        [StringLength(256)]
        public string Alias { get; set; }
        [StringLength(256)]
        public string PrimaryDrugs { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LockedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [Range(0, long.MaxValue)]
        public long? TotalSubjects { get; set; }
        [Range(0, long.MaxValue)]
        public long? AnimalSpeciesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? ImpressionId { get; set; }

        public int TotalQueriesPending { get; set; }
        public int TotalQueriesFlagged { get; set; }

        public AnimalSpeciesFullDto AnimalSpecies { get; set; }
        public ImpressionFullDto Impression { get; set; }
    }
}

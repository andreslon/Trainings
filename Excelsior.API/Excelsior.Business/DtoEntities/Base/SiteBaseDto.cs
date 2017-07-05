using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class SiteBaseDto
    {
        public SiteBaseDto()
             : this(null)
        {

        }
        public SiteBaseDto(PACS_Site entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.SiteID;
                RandomizedId = entity.RandomizedSiteID;
                IsActive = entity.IsActive;
                IsIRB = entity.IsIRB;
                IsTesting = entity.IsTestingSite;
                PrincipalInvestigator = entity.PrincipalInvestigator;
                StudyId = entity.TrialID;
                AffiliationId = entity.AffiliationID;

                if (!(sender is AffiliationBaseDto) && entity.CONTACTAffiliation != null)
                {
                    Affiliation = new AffiliationFullDto(entity.CONTACTAffiliation, this);
                }
            }
        }
        public virtual PACS_Site ToEntity(PACS_Site entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new PACS_Site();
            }

            entity.SiteID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["randomizedid"])
                    entity.RandomizedSiteID = RandomizedId;
                if (fieldvalidation["principalinvestigator"])
                    entity.PrincipalInvestigator = PrincipalInvestigator;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["isIrb"])
                    entity.IsIRB = IsIRB.GetValueOrDefault();
                if (fieldvalidation["istesting"])
                    entity.IsTestingSite = IsTesting.GetValueOrDefault();
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["affiliationid"])
                    entity.AffiliationID = AffiliationId;

            }


            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [StringLength(50)]
        public string RandomizedId { get; set; }
        public string PrincipalInvestigator { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsIRB { get; set; }
        public bool? IsTesting { get; set; }
        [Range(0, long.MaxValue)]
        public long? TotalSubjects { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        [Range(0, long.MaxValue)]
        public long? AffiliationId { get; set; }

        public int TotalQueriesPending { get; set; }
        public int TotalQueriesFlagged { get; set; }

        public AffiliationFullDto Affiliation { get; set; }
    }
}
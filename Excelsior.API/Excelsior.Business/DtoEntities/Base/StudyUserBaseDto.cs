using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class StudyUserBaseDto
    {
        public StudyUserBaseDto()
            : this(null)
        {

        }
        public StudyUserBaseDto(CONTACT_UserTrial entity, object sender = null)
        {
            if (entity != null)
            {
                UserId = entity.UserID;
                StudyId = entity.TrialID;
                Id = entity.UserTrialID;
                IsActive = entity.IsActive;
                StudyName = entity.PACSTrial.TrialName;
                StudyAlias = entity.PACSTrial.TrialAlias;

                if (!(sender is UserBaseDto) && entity.CONTACTUser != null)
                {
                    User = new UserFullDto(entity.CONTACTUser, this);
                }
            }
        }
        public virtual CONTACT_UserTrial ToEntity(CONTACT_UserTrial entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new CONTACT_UserTrial();
            }
            entity.UserTrialID = Id.GetValueOrDefault();
            
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["userid"])
                    entity.UserID = UserId;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault();
            }
           
            return entity;
        }
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? UserId { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        public bool? IsActive { get; set; }

        public string StudyName { get; set; }
        public string StudyAlias { get; set; }

        public UserFullDto User { get; set; }
    }
}

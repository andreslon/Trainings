using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class UserBaseDto
    {
        public UserBaseDto()
            : this(null)
        {

        }
        public UserBaseDto(CONTACT_User entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.UserID;
                AspnetUserId = entity.AspUserID;
                UserName = entity.AspnetUser?.UserName;
                FirstName = entity.FirstName;
                MiddleName = entity.MiddleName;
                LastName = entity.LastName;
                Email = entity.Email;
                IsActive = entity.IsActive;
                AffiliationId = entity.AffiliationID;
                RoleId = entity.RoleId;

                if (!(sender is AffiliationBaseDto) && entity.CONTACTAffiliation != null)
                {
                    Affiliation = new AffiliationFullDto(entity.CONTACTAffiliation, this);
                }

                if (!(sender is RoleBaseDto) && entity.AspnetRole != null)
                {
                    Role = new RoleFullDto(entity.AspnetRole, this);
                }
            }
        }
        public virtual CONTACT_User ToEntity(CONTACT_User entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CONTACT_User();
            }

            entity.UserID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["firstname"])
                    entity.FirstName = FirstName;
                if (fieldvalidation["middlename"])
                    entity.MiddleName = MiddleName;
                if (fieldvalidation["lastname"])
                    entity.LastName = LastName;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["roleid"])
                    entity.RoleId = RoleId;
                if (fieldvalidation["affiliationid"])
                    entity.AffiliationID = AffiliationId; 
                if (fieldvalidation["jobtitle"])
                    entity.JobTitle = JobTitle;
                if (fieldvalidation["email"])
                {
                    entity.Email = Email;
                    entity.LoweredEmail = Email?.ToLower();
                    if (entity.AspUserID != null)
                    {
                        var am = entity.AspnetUser as Aspnet_Membership;
                        am.Email = entity.Email;
                        am.LoweredEmail = entity.LoweredEmail;
                    }
                }
            }

            return entity;
        }

        public long? Id { get; set; }
        public Guid? AspnetUserId { get; set; }
        [StringLength(256)]
        [Required]
        public string UserName { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(200)]
        public string MiddleName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [StringLength(50)]
        public string JobTitle { get; set; }
        public bool? IsActive { get; set; }
        [Range(0, long.MaxValue)]
        public long? AffiliationId { get; set; } 
        public Guid? RoleId { get; set; }
        public AffiliationFullDto Affiliation { get; set; }
        public RoleFullDto Role { get; set; }
    }
}

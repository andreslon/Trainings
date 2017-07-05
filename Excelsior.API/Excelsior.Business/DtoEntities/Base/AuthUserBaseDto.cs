using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuthUserBaseDto
    {
        public AuthUserBaseDto()
            : this(null)
        {

        }
        public AuthUserBaseDto(Aspnet_Membership user, object sender = null)
        {
            if (user != null)
            {
                PasswordFormat = user.PasswordFormat;
                PasswordSalt = user.PasswordSalt;
                Password = user.Password;
                ClaimType = "";
                Company = user.CONTACT_Users?.FirstOrDefault().CONTACTAffiliation?.AffiliationName;
                Email = user.CONTACT_Users?.FirstOrDefault()?.Email;
                EmailVerified = "true";
                FamilyName = user.CONTACT_Users?.FirstOrDefault()?.LastName;
                Firstname = user.CONTACT_Users?.FirstOrDefault()?.FirstName;
                GivenName = user.CONTACT_Users?.FirstOrDefault()?.FirstName;
                Name = user.CONTACT_Users?.FirstOrDefault()?.FirstName;
                UserName = user.UserName;
                Role = user.CONTACT_Users?.FirstOrDefault()?.AspnetRole?.RoleName;
                Surname = user.CONTACT_Users?.FirstOrDefault()?.LastName;
                Id = user.UserId;
                WebSite = "";
            }
        }
        public virtual Aspnet_Membership ToEntity(Aspnet_Membership entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new Aspnet_Membership();
            }
            entity.UserId = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["passwordformat"])
                    entity.PasswordFormat = PasswordFormat.GetValueOrDefault();
                if (fieldvalidation["passwordsalt"])
                    entity.PasswordSalt = PasswordSalt;
                if (fieldvalidation["username"])
                    entity.UserName = UserName;
                if (fieldvalidation["password"])
                    entity.Password = Password;
                if (fieldvalidation["passwordquestion"])
                    entity.PasswordQuestion = PasswordQuestion;
                if (fieldvalidation["passwordanswer"])
                    entity.PasswordAnswer = PasswordAnswer;
            }
                   
           
            return entity;
        }

        public string Company { get; set; }
        public string Email { get; set; }
        public string EmailVerified { get; set; }
        public string FamilyName { get; set; }
        public string Firstname { get; set; }
        public string GivenName { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Surname { get; set; }
        public Guid Id { get; set; }
        public string WebSite { get; set; }
        public string ClaimType { get; set; }
        public int? PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        [StringLength(200)]
        public string Password { get; set; }
        [StringLength(200)]
        public string PasswordQuestion { get; set; }
        [StringLength(200)]
        public string PasswordAnswer { get; set; }
    }
}

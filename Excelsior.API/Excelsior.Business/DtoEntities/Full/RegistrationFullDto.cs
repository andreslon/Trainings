using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Full
{
    public class RegistrationFullDto : UserFullDto
    {
        public Aspnet_User ToUserEntity(Aspnet_User entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new Aspnet_User();
            }
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["username"])
                {
                    entity.UserName = UserName;
                    entity.LoweredUserName = UserName;
                }
            }
            return entity;
        }
        public Aspnet_Membership ToMembershipEntity(Aspnet_Membership entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new Aspnet_Membership();
            }
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["username"])
                {
                    entity.UserName = UserName;
                    entity.LoweredUserName = UserName;
                }
                if (fieldvalidation["password"])
                    entity.Password = Password;
                if (fieldvalidation["passwordquestion"])
                    entity.PasswordQuestion = PasswordQuestion;
                if (fieldvalidation["passwordanswer"])
                    entity.PasswordAnswer = PasswordAnswer;
                if (fieldvalidation["email"])
                {
                    entity.Email = Email;
                    entity.LoweredEmail = Email;
                }
            }
            return entity;
        }

        [StringLength(200)]
        [Required]
        public string Password { get; set; }
        [StringLength(200)]
        [Required]
        public string PasswordConfirmation { get; set; }
        [StringLength(200)]
        [Required]
        public string PasswordQuestion { get; set; }
        [StringLength(200)]
        [Required]
        public string PasswordAnswer { get; set; }
    }
}

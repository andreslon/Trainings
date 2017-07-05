using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AuthRecoveryDataFullDto : AuthRecoveryDataBaseDto
    {
        public AuthRecoveryDataFullDto()
            : this(null)
        {
        }
        public AuthRecoveryDataFullDto(Aspnet_Membership entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                Email = entity.CONTACT_Users?.FirstOrDefault()?.Email;
            }
        }
         
        public string Email { get; set; }
        public string Answer { get; set; }
         
        [RegularExpression("^.*[^a-zA-Z0-9].*$", ErrorMessage = "Password needs to contain at least one special character e.g. @ or #.")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Password must be at least 7 and at most 50 characters long.")]
        [Required]
        public string NewPassword { get; set; }

        [RegularExpression("^.*[^a-zA-Z0-9].*$", ErrorMessage = "Password needs to contain at least one special character e.g. @ or #.")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Password must be at least 7 and at most 50 characters long.")]
        [Required]
        public string PasswordConfirmation { get; set; }
    }
}

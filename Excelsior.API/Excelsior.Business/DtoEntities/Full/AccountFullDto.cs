using Excelsior.Business.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AccountFullDto : AccountBaseDto
    {
        [RegularExpression("^.*[^a-zA-Z0-9].*$", ErrorMessage = "Password needs to contain at least one special character e.g. @ or #.")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Password must be at least 7 and at most 50 characters long.")]
        [Required]
        public string CurrentPassword { get; set; }

        [RegularExpression("^.*[^a-zA-Z0-9].*$", ErrorMessage = "Password needs to contain at least one special character e.g. @ or #.")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Password must be at least 7 and at most 50 characters long.")]
        public string NewPassword { get; set; }

        [RegularExpression("^.*[^a-zA-Z0-9].*$", ErrorMessage = "Password needs to contain at least one special character e.g. @ or #.")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Password must be at least 7 and at most 50 characters long.")]
        public string PasswordConfirmation { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "The PIN must be 4 number")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "The PIN must be 4 number")]
        public string NewPin { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "The PIN must be 4 number")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "The PIN must be 4 number")]
        public string PinConfirmation { get; set; }

        [StringLength(128, ErrorMessage = "The password question is not formatted correctly.")]
        public string PasswordQuestion { get; set; }
        [StringLength(128, ErrorMessage = "The password answer is not formatted correctly.")]
        public string PasswordAnswer { get; set; }
    }
}

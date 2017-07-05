using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuthRecoveryDataBaseDto
    {
        public AuthRecoveryDataBaseDto()
            : this(null)
        {

        }
        public AuthRecoveryDataBaseDto(Aspnet_Membership entity, object sender = null)
        {
            if (entity != null)
            {
                PasswordQuestion = entity.PasswordQuestion;
                UserName = entity.UserName;
            }
        } 
        public string UserName { get; set; }
        public string PasswordQuestion { get; set; }
    }
}

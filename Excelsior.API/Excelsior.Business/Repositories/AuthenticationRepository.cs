using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.Logic;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Repositories
{
    public class AuthenticationRepository 
    {
        public DataModel Context { get; set; }

        public AuthenticationRepository(DataModel context)
        {
            Context = context;
        }

        public UserRequestDto FindUser(string userName)
        {
            var result = new UserRequestDto();
            var handler = new UserHandler(Context);
            var user = handler.GetUser(userName);
            if (user != null)
            {
                var cUser = user.CONTACT_Users.FirstOrDefault();
                result = new UserRequestDto()
                {
                    PasswordFormat = (user as Aspnet_Membership).PasswordFormat,
                    PasswordSalt = user.PasswordSalt,
                    Password = user.Password,
                    ClaimType = "",
                    Company = cUser?.CONTACTAffiliation?.AffiliationName,
                    Email = cUser?.Email,
                    EmailVerified = "true",
                    FamilyName = cUser?.LastName,
                    Firstname = cUser?.FirstName,
                    GivenName = cUser?.FirstName,
                    Name = cUser?.FirstName,
                    NickName = user.UserName,
                    Role = cUser?.AspnetRole?.RoleName,
                    Surname = cUser?.LastName,
                    UserId = user.UserId,
                    WebSite = ""
                };

            }
            return result;
        }

        public AUTH_Client FindClient(string clientId)
        {
            var handler = new UserHandler(Context);
            return handler.GetClient(clientId);
        }
        
        public List<AUTH_Scope> GetScopes()
        {
            var handler = new UserHandler(Context);
            return handler.GetScopes();
        }
    }
}
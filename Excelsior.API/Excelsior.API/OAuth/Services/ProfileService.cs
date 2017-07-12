using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;
using IdentityModel;
using Excelsior.Business.Repositories;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Business.Gateways;

namespace Excelsior.API.OAuth.Services
{
    public class ProfileService : IProfileService
    {
        private IAuthUserGateway AuthUserGateway { get; set; }
        public ProfileService(IAuthUserGateway authUserGateway)
        {
            AuthUserGateway = authUserGateway;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string subject = context.Subject.Claims.ToList().Find(s => s.Type == JwtClaimTypes.Subject).Value;

            if (subject != null)
            {

                var response = AuthUserGateway.GetSingle(Guid.Parse(subject));
                if (response.IsSuccess)
                { 
                    //var user = response.Result.ToEntity();

                    DataModel DataModel = new DataModel();
                    var user = DataModel.CONTACT_Users.FirstOrDefault(x => x.AspnetUser.UserId.ToString() == subject);


                    var claims = new Claim[]
                            {
                                    new Claim(JwtClaimTypes.Subject, subject),
                                    new Claim(JwtClaimTypes.Role, user.AspnetRole.RoleName),
                            }.AsEnumerable();
                    context.IssuedClaims = claims.ToList();
                }
            }
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            
            

            if (context.Subject == null) throw new ArgumentNullException("subject");

            string subject = context.Subject.Claims.ToList().Find(s => s.Type == JwtClaimTypes.Subject).Value;

             

            context.IsActive = (subject != null)  ;

            return Task.FromResult(0);
        }
    }
}

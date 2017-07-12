using Excelsior.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Excelsior.API.OAuth.Extensions
{
    public class ResourceOwnerData : IResourceOwnerData
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public ResourceOwnerData(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            ClaimsPrincipal User = HttpContextAccessor.HttpContext.User;

            string userId = User?
                .Claims?
                .ToList()?
                .Find(s => s.Type == "sub")?
                .Value;

            return userId;
        }
        public string GetClientId()
        {
            ClaimsPrincipal User = HttpContextAccessor.HttpContext.User;

            string clientid = User?
                .Claims?
                .ToList()?
                .Find(s => s.Type == "client_id")?
                .Value;

            return clientid;
        }
    }
}

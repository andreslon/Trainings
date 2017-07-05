using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AuthScopeFullDto : AuthScopeBaseDto
    {
        public AuthScopeFullDto()
            : this(null)
        {
            ClientScopes = new List<AuthClientScopeFullDto>();
            ScopeClaims = new List<AuthScopeClaimFullDto>();
        }
        public AuthScopeFullDto(AUTH_Scope entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            { 
                if (!(sender is AuthClientScopeBaseDto) && entity.AUTH_ClientScopes.Count > 0)
                {
                    ClientScopes = new List<AuthClientScopeFullDto>();
                    foreach (var item in entity.AUTH_ClientScopes)
                    {
                        ClientScopes.Add(new AuthClientScopeFullDto(item, this));
                    }
                }
                if (!(sender is AuthScopeClaimBaseDto) && entity.AUTH_ScopeClaims.Count > 0)
                {
                    ScopeClaims = new List<AuthScopeClaimFullDto>();
                    foreach (var item in entity.AUTH_ScopeClaims)
                    {
                        ScopeClaims.Add(new AuthScopeClaimFullDto(item, this));
                    }
                }
            }
        }
        public override AUTH_Scope ToEntity(AUTH_Scope entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            if (ClientScopes.Count > 0)
            {
                entity.AUTH_ClientScopes.Clear();
                foreach (var a in ClientScopes)
                {
                    var lde = a.ToEntity();
                    entity.AUTH_ClientScopes.Add(lde);
                }
            }
            if (ScopeClaims.Count > 0)
            {
                entity.AUTH_ScopeClaims.Clear();
                foreach (var a in ScopeClaims)
                {
                    var lde = a.ToEntity();
                    entity.AUTH_ScopeClaims.Add(lde);
                }
            }
            return entity;
        }
        public List<AuthClientScopeFullDto> ClientScopes { get; set; }
        public List<AuthScopeClaimFullDto> ScopeClaims { get; set; } 
    }
}

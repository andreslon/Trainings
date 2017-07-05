using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AuthClientFullDto : AuthClientBaseDto
    {
        public AuthClientFullDto()
            : this(null)
        {
            ClientScopes = new List<AuthClientScopeFullDto>();
            ClientSecrets = new List<AuthClientSecretFullDto>();
        }
        public AuthClientFullDto(AUTH_Client entity, object sender = null)
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
                if (!(sender is AuthClientSecretBaseDto) && entity.AUTH_ClientSecrets.Count > 0)
                {
                    ClientSecrets = new List<AuthClientSecretFullDto>();
                    foreach (var item in entity.AUTH_ClientSecrets)
                    {
                        ClientSecrets.Add(new AuthClientSecretFullDto(item, this));
                    }

                }
            }
        }
        public override AUTH_Client ToEntity(AUTH_Client entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);
            if (ClientScopes.Count > 0)
            {
                entity.AUTH_ClientScopes.Clear();
                foreach (var a in ClientScopes)
                {
                    var lde = a.ToEntity();
                    entity.AUTH_ClientScopes.Add(lde);
                }
            }
            if (ClientSecrets.Count > 0)
            {
                entity.AUTH_ClientSecrets.Clear();
                foreach (var a in ClientSecrets)
                {
                    var lde = a.ToEntity();
                    entity.AUTH_ClientSecrets.Add(lde);
                }
            }
            return entity;
        }

        //public Aspnet_User ApnetUser { get; set; }
        public List<AuthClientScopeFullDto> ClientScopes { get; set; }
        public List<AuthClientSecretFullDto> ClientSecrets { get; set; }

    }
}

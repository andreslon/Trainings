using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Excelsior.Domain;
using Excelsior.Business.Repositories;
using IdentityServer4.Stores;
using Excelsior.Business.Gateways;
using static IdentityServer4.IdentityServerConstants;
using Excelsior.Infrastructure.Interfaces;

namespace Excelsior.API.OAuth.Services
{
    public class ClientStore : IClientStore
    {
        public IAuthClientGateway AuthClientGateway { get; set; }
        public ISettings Settings { get; set; }
        public ClientStore(IAuthClientGateway authClientGateway, ISettings settings)
        {
            AuthClientGateway = authClientGateway;
            Settings = settings;
        }
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var result = new Client();
            Guid r;
            if (!Guid.TryParse(clientId, out r))
            {
                return Task.FromResult(result);
            }

            var response = AuthClientGateway.GetSingle(Guid.Parse(clientId));
            if (!response.IsSuccess)
            {
                return Task.FromResult(result);
            }

            var client = response.Result;
            try
            {
                if (client == null)
                {
                    return null;
                }
                else
                {
                    result = new Client
                    {
                        ClientId = client.Id.ToString(),
                        ClientName = client.Name,
                        ClientSecrets = new List<Secret>(),
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                        AllowOfflineAccess = true,
                        AccessTokenLifetime = (int)TimeSpan.FromMinutes(int.Parse(Settings.GetSetting("TokenLifetime"))).TotalSeconds,
                        AllowedScopes =
                        {
                            StandardScopes.OpenId,
                            StandardScopes.Email,
                            StandardScopes.Profile,
                            StandardScopes.OfflineAccess,
                        }
                    };
                    if (client.ClientSecrets != null)
                    {
                        foreach (var secret in client.ClientSecrets)
                        {
                            result.ClientSecrets.Add(new Secret(secret.ClientSecret.Sha256()));
                        }
                    }
                    if (client.ClientScopes != null)
                    {
                        foreach (var clientScope in client.ClientScopes)
                        {
                            result.AllowedScopes.Add(clientScope.Name);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return Task.FromResult(result);
        }
    }
}

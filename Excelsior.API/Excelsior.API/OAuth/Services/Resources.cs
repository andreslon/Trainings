using Excelsior.Business.Gateways;
using Excelsior.Domain;
using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Excelsior.API.OAuth.Services
{
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                // some standard scopes from the OIDC spec
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                // custom identity resource with some consolidated claims
                new IdentityResource("ExcelsiorProfile", new[]
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email
                })
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("webapi") {
                        UserClaims =
                            {
                                JwtClaimTypes.GivenName,
                                JwtClaimTypes.FamilyName,
                                JwtClaimTypes.Name,
                                JwtClaimTypes.Email,
                            }
                },
                new ApiResource(StandardScopes.OpenId),
                new ApiResource(StandardScopes.Profile),
                new ApiResource(StandardScopes.OfflineAccess)
            };
            //IEnumerable<ApiResource> result = new List<ApiResource>();
            //result = new List<ApiResource>()
            //            {
            //            new ApiResource(StandardScopes.OpenId),
            //            new ApiResource(StandardScopes.Profile),
            //            new ApiResource(StandardScopes.OfflineAccess)
            //    };
            //List<AUTH_Scope> scopes;
            //var response = AuthScopeGateway.GetAll(new Business.DtoEntities.Request.AuthScopesRequestDto());
            //if (response.IsSuccess)
            //{
            //    scopes = new List<AUTH_Scope>();
            //    foreach (var item in response.Result)
            //    {
            //        scopes.Add(item.ToEntity());
            //    }
            //}
            //else
            //{
            //    return result;
            //}
            //foreach (var scope in scopes)
            //{
            //    var newScope = new Scope
            //    {
            //        Name = scope.Name,
            //        DisplayName = scope.DisplayName,
            //        Description = scope.Description,
            //        UserClaims = new List<string>()
            //    };
            //    foreach (var claim in scope.AUTH_ScopeClaims)
            //    {
            //        newScope.UserClaims.Add(claim.Name);
            //    }

            //    ((List<Scope>)result).Add(newScope);
            //}
            //return result;
        }

        //public class ScopeStore : IScopeStore
        //{
        //    private IAuthScopeGateway AuthScopeGateway { get; set; }
        //    public ScopeStore(IAuthScopeGateway authScopeGateway)
        //    {
        //        AuthScopeGateway = authScopeGateway;
        //    }
        //    public Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        //    {
        //        IEnumerable<Scope> result = new List<Scope>() {
        //            // standard OpenID Connect scopes
        //            StandardScopes.OpenId,
        //            StandardScopes.ProfileAlwaysInclude,
        //            StandardScopes.EmailAlwaysInclude,
        //            StandardScopes.OfflineAccess,
        //            StandardScopes.RolesAlwaysInclude,
        //        };
        //        List<AUTH_Scope> scopes;
        //        var response = AuthScopeGateway.GetAll(new Business.DtoEntities.Request.AuthScopesRequestDto());
        //        if (response.IsSuccess)
        //        {
        //            scopes = new List<AUTH_Scope>();
        //            foreach (var item in response.Result)
        //            {
        //                scopes.Add(item.ToEntity());
        //            }
        //        }
        //        else
        //        {
        //            return Task.FromResult(result);
        //        }
        //        IEnumerable<AUTH_Scope> scopesFiltered;
        //        try
        //        {
        //            if (scopeNames != null && scopeNames.Any())
        //            {
        //                scopesFiltered = from s in scopes
        //                                 where scopeNames.Contains(s.Name)
        //                                 select s;
        //                foreach (var scope in scopesFiltered)
        //                {
        //                    var newScope = new Scope
        //                    {
        //                        Name = scope.Name,
        //                        DisplayName = scope.DisplayName,
        //                        Description = scope.Description,
        //                        Type = ScopeType.Resource,
        //                        ScopeSecrets = new List<Secret>
        //                        {
        //                            new Secret("secret".Sha256())
        //                        },
        //                        Claims = new List<ScopeClaim>()
        //                    };
        //                    foreach (var claim in scope.AUTH_ScopeClaims)
        //                    {
        //                        newScope.Claims.Add(new ScopeClaim(claim.Name, claim.AlwaysIncludeInIdToken));
        //                    }

        //                    ((List<Scope>)result).Add(newScope);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //        return Task.FromResult(result);
        //    }

        //    public Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        //    {
        //        IEnumerable<Scope> result = new List<Scope>();
        //        if (publicOnly)
        //        {
        //            result = new List<Scope>()
        //            {
        //                StandardScopes.OpenId,
        //                StandardScopes.ProfileAlwaysInclude,
        //                StandardScopes.EmailAlwaysInclude,
        //                StandardScopes.OfflineAccess,
        //                StandardScopes.RolesAlwaysInclude,
        //            };

        //            List<AUTH_Scope> scopes;
        //            var response = AuthScopeGateway.GetAll(new Business.DtoEntities.Request.AuthScopesRequestDto());
        //            if (response.IsSuccess)
        //            {
        //                scopes = new List<AUTH_Scope>();
        //                foreach (var item in response.Result)
        //                {
        //                    scopes.Add(item.ToEntity());
        //                }
        //            }
        //            else
        //            {
        //                return Task.FromResult(result);
        //            }
        //            foreach (var scope in scopes)
        //            {
        //                var newScope = new Scope
        //                {
        //                    Name = scope.Name,
        //                    DisplayName = scope.DisplayName,
        //                    Description = scope.Description,
        //                    Type = ScopeType.Resource,
        //                    ScopeSecrets = new List<Secret>
        //                {
        //                    new Secret("secret".Sha256())
        //                },
        //                    Claims = new List<ScopeClaim>()
        //                };
        //                foreach (var claim in scope.AUTH_ScopeClaims)
        //                {
        //                    newScope.Claims.Add(new ScopeClaim(claim.Name, claim.AlwaysIncludeInIdToken));
        //                }

        //                ((List<Scope>)result).Add(newScope);
        //            }
        //        }

        //        return Task.FromResult(result);
        //    }
        //}
    }
}

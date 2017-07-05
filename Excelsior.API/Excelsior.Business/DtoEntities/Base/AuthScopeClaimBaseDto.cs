using Excelsior.Domain;
using System;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuthScopeClaimBaseDto
    {
        public AuthScopeClaimBaseDto()
            : this(null)
        {

        }
        public AuthScopeClaimBaseDto(AUTH_ScopeClaim entity, object sender = null)
        {
            if (entity != null)
            {
                ScopeClaimId = entity.ScopeClaimId;
                Name = entity.Name;
                Description = entity.Description;
                AlwaysIncludeInIdToken = entity.AlwaysIncludeInIdToken;
                ScopeId = entity.ScopeId;
            }
        }
        public virtual AUTH_ScopeClaim ToEntity(AUTH_ScopeClaim entity = null)
        {
            if (entity == null)
            {
                entity = new AUTH_ScopeClaim();
            }


            entity.ScopeClaimId = ScopeClaimId;
            entity.Name = Name;
            entity.Description = Description;
            entity.AlwaysIncludeInIdToken = AlwaysIncludeInIdToken;
            entity.ScopeId = ScopeId;

            return entity;
        }

        public Guid ScopeClaimId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AlwaysIncludeInIdToken { get; set; }
        public Guid ScopeId { get; set; }
         
    }
}

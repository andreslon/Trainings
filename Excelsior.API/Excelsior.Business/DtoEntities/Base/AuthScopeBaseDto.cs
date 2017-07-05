using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuthScopeBaseDto
    {
        public AuthScopeBaseDto()
            : this(null)
        {

        }
        public AuthScopeBaseDto(AUTH_Scope entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.ScopeId;
                Name = entity.Name;
                DisplayName = entity.DisplayName;
                Description = entity.Description;
            }
        }
        public virtual AUTH_Scope ToEntity(AUTH_Scope entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new AUTH_Scope();
            }

            entity.ScopeId = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.Name = Name;
                if (fieldvalidation["displayname"])
                    entity.DisplayName = DisplayName;
                if (fieldvalidation["description"])
                    entity.Description = Description;
            }


            return entity;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }



    }
}

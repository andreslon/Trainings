using Excelsior.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class RoleBaseDto
    {
        public RoleBaseDto()
            : this(null)
        {

        }
        public RoleBaseDto(Aspnet_Role entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.RoleId;
                Name = entity.RoleName;
            }
        }
        public virtual Aspnet_Role ToEntity(Aspnet_Role entity = null)
        {
            if (entity == null)
            {
                entity = new Aspnet_Role();
            }

            entity.RoleId = Id.GetValueOrDefault();
            entity.RoleName = Name;

            return entity;
        }

        public Guid? Id { get; set; }
        [StringLength(256)]
        [Required]
        public string Name { get; set; }
    }
}

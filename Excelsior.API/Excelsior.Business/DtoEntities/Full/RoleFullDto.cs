using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Full
{
    public class RoleFullDto : RoleBaseDto
    {
        public RoleFullDto()
            : this(null)
        {

        }
        public RoleFullDto(Aspnet_Role entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                LoweredName = entity.LoweredRoleName;
                Description = entity.Description;
            }
        }
        public override Aspnet_Role ToEntity(Aspnet_Role entity = null)
        {
            entity = base.ToEntity(entity);

            entity.LoweredRoleName = LoweredName;
            entity.Description = Description;

            return entity;
        }
        [StringLength(256)]
        public string LoweredName { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
    }
}

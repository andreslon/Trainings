using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Base
{
    public class DocumentRoleBaseDto
    {
        public DocumentRoleBaseDto()
            : this(null)
        {

        }
        public DocumentRoleBaseDto(DOCU_DocumentRole entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.DocumentRoleID;
                DocumentId = entity.DocumentID;
                RoleId = entity.RoleId;
                Name = entity.AspnetRole.RoleName;

            }
        }
        public virtual DOCU_DocumentRole ToEntity(DOCU_DocumentRole entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new DOCU_DocumentRole();
            }

            entity.DocumentRoleID = Id.GetValueOrDefault();
            entity.DocumentID = DocumentId;
            entity.RoleId = RoleId;

            return entity;
        }
        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Required]
        [Range(0, long.MaxValue)]
        public long? DocumentId { get; set; }
        [Required]
        public Guid? RoleId { get; set; }
        [StringLength(512)]
        public string Name { get; set; }
    }
}

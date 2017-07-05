using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Base
{
    public class DocumentVersionBaseDto
    {
        public DocumentVersionBaseDto()
            : this(null)
        {

        }
        public DocumentVersionBaseDto(DOCU_DocumentVersion entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.DocumentVersionID;
                Version = entity.DocumentVersion;
                FileLocation = entity.FileLocation;
                DocumentId = entity.DOCUDocument.DocumentID;
                IsActive = entity.IsActive;
                AttachmentFileLocation = entity.AttachmentFileLocation;
                StatusId = entity.StatusID;
                attachemntStatusId = entity.AttachmentStatusID;
                HasError = entity.HasError;
                LastError = entity.LastError;
            }
        }
        public virtual DOCU_DocumentVersion ToEntity(DOCU_DocumentVersion entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new DOCU_DocumentVersion();
            }
            entity.DocumentID = DocumentId;

            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["version"])
                    entity.DocumentVersion = Version;
                if (fieldvalidation["filelocation"])
                    entity.FileLocation = FileLocation;
                if (fieldvalidation["attachmentfilelocation"])
                    entity.AttachmentFileLocation = AttachmentFileLocation;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["haserror"])
                    entity.HasError = HasError.GetValueOrDefault();
                if (fieldvalidation["lasterror"])
                    entity.LastError = LastError;
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [StringLength(50)]
        public string Version { get; set; }
        [StringLength(512)]
        public string FileLocation { get; set; }
        [Range(0, long.MaxValue)]
        public long? DocumentId { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(512)]
        public string AttachmentFileLocation { get; set; }
        [Range(0, long.MaxValue)]
        public long StatusId { get; set; }
        [Range(0, long.MaxValue)]
        public long attachemntStatusId { get; set; }
        public bool? HasError { get; set; }
        public string LastError { get; set; }
    }
}

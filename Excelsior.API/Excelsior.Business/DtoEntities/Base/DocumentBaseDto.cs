using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Base
{
    public class DocumentBaseDto
    {
        public DocumentBaseDto()
            : this(null)
        {

        }
        public DocumentBaseDto(DOCU_Document entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.DocumentID;
                Name = entity.DocumentName;
                IsActive = entity.IsActive;
                ApprovalDate = entity.ApprovalDate;
                StudyId = entity.DOCUDocumentGroup.TrialID;

                if (!(sender is DocumentVersionBaseDto) && entity.DOCU_DocumentVersions != null)
                {
                    var lvEntity = entity.DOCU_DocumentVersions.LastOrDefault(x => x.IsActive);
                    if (lvEntity != null)
                    {
                        LatestVersion = new DocumentVersionFullDto(lvEntity, this);
                        //Assign the default urls
                        if (string.IsNullOrEmpty(LatestVersion.FileLocation))
                            LatestVersion.FileLocation = string.Format("{0}/documents/{1}/file_{2}.", entity.DOCUDocumentGroup.TrialID, entity.DocumentID, lvEntity.DocumentVersion); ;
                        if (string.IsNullOrEmpty(LatestVersion.AttachmentFileLocation))
                            LatestVersion.AttachmentFileLocation = string.Format("{0}/documents/{1}/attachment_{2}.", entity.DOCUDocumentGroup.TrialID, entity.DocumentID, lvEntity.DocumentVersion);
                        LatestVersion.StatusId = 1;
                        LatestVersion.attachemntStatusId = 1;
                    }
                }
            }
        }
        public virtual DOCU_Document ToEntity(DOCU_Document entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new DOCU_Document();
            }

            entity.DocumentID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.DocumentName = Name;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["approvalDate"])
                    entity.ApprovalDate = ApprovalDate;
            }   
            return entity;
        }
        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Name { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ApprovalDate { get; set; }
        public DocumentVersionFullDto LatestVersion { get; set; }
        public bool? IsActive { get; set; }
    }
}

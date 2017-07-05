using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class CertQuestionBaseDto
    {
        public CertQuestionBaseDto()
            : this(null)
        {
        }
        public CertQuestionBaseDto(CERT_QuestionList entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CertQuestionListID;
                Name = entity.CertQuestionDes;
                ProcedureId = entity.ImgProcedureID;

                if (!(sender is ProcedureBaseDto) && entity.CERTImgProcedureList != null)
                {
                    Procedure = new ProcedureFullDto(entity.CERTImgProcedureList, this);
                }
            }
        }
        public virtual CERT_QuestionList ToEntity(CERT_QuestionList entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new CERT_QuestionList();
            }
            entity.CertQuestionListID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            { 
                if (fieldvalidation["name"])
                    entity.CertQuestionDes = Name;
                if (fieldvalidation["procedureid"])
                    entity.ImgProcedureID = ProcedureId;
            }
                   
            return entity;
        }
        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        [Range(0, long.MaxValue)]
        public long? ProcedureId { get; set; }

        public ProcedureFullDto Procedure { get; set; }
    }
}
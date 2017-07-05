using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class CertQuestionFullDto : CertQuestionBaseDto
    {
        public CertQuestionFullDto()
            : this(null)
        {
        }
        public CertQuestionFullDto(CERT_QuestionList entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override CERT_QuestionList ToEntity(CERT_QuestionList entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
}
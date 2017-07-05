using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AnswerTypeFullDto : AnswerTypeBaseDto
    {
        public AnswerTypeFullDto()
            : this(null)
        {
        }
        public AnswerTypeFullDto(CRF_AnswerType entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override CRF_AnswerType ToEntity(CRF_AnswerType entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
} 

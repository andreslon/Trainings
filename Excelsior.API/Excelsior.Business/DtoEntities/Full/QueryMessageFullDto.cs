using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class QueryMessageFullDto : QueryMessageBaseDto
    {
        public QueryMessageFullDto()
            : this(null)
        {

        }
        public QueryMessageFullDto(QRY_Message entity, object sender = null)
            : base(entity, sender)
        {

        }
        public override QRY_Message ToEntity(QRY_Message entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);

            return entity;
        }
    }
} 

using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Full
{
    public class QueryFullDto : QueryBaseDto
    {
        public QueryFullDto()
            : this(null)
        {

        }
        public QueryFullDto(QRY_Query entity, object sender = null)
            : base(entity, sender)
        {
            Messages = new List<QueryMessageFullDto>();

            if (entity != null)
            {
                if (!(sender is GradingResultBaseDto) && entity.QRY_Messages.Count > 0)
                {
                    var msgs = entity.QRY_Messages.OrderByDescending(x => x.DateCreated);
                    foreach (var msg in msgs)
                    {
                        Messages.Add(new QueryMessageFullDto(msg));
                    }
                }
            }
        }

        public override QRY_Query ToEntity(QRY_Query entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);

            return entity;
        }

        public List<QueryMessageFullDto> Messages { get; set; }
    }
}
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class QueryMessageBaseDto
    {
        public QueryMessageBaseDto()
            : this(null)
        {
        }
        public QueryMessageBaseDto(QRY_Message entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.QueryID;
                CreatedById = entity.UserID;
                QueryId = entity.QueryID;
                Body = entity.MessageBody;
                IsActive = entity.IsActive;
                DateCreated = entity.DateCreated;

                if (!(sender is UserBaseDto) && entity.CONTACTUser != null)
                {
                    CreatedBy = new UserFullDto(entity.CONTACTUser, this);
                }
            }
        }
        public virtual QRY_Message ToEntity(QRY_Message entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new QRY_Message();
            }
            entity.MessageID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["createdbyid"])
                    entity.UserID = CreatedById;
                if (fieldvalidation["queryid"])
                    entity.QueryID = QueryId;
                if (fieldvalidation["body"])
                    entity.MessageBody = Body;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["datecreated"])
                    entity.DateCreated = DateCreated;
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? CreatedById { get; set; }
        [Range(0, long.MaxValue)]
        public long? QueryId { get; set; }
        [Required]
        public string Body { get; set; }
        public bool? IsActive { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        public UserFullDto CreatedBy { get; set; }
    }
}
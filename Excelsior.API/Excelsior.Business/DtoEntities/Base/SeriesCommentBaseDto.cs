using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class SeriesCommentBaseDto
    {
        public SeriesCommentBaseDto()
            : this(null)
        {
        }
        public SeriesCommentBaseDto(PACS_SeriesComment entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.SCommentID;
                Value = entity.CommentText;
                Date = entity.CreatedDate;
                CreatedById = entity.UserID;
                SeriesId = entity.SeriesID;

                if (!(sender is UserBaseDto) && entity.CONTACTUser != null)
                {
                    CreatedBy = new UserFullDto(entity.CONTACTUser, this);
                }
            }
        }
        public virtual PACS_SeriesComment ToEntity(PACS_SeriesComment entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_SeriesComment();
            }
            entity.SCommentID = Id.GetValueOrDefault();
            entity.CommentText = Value;
            entity.CreatedDate = Date;
            entity.UserID = CreatedById;
            entity.SeriesID = SeriesId;
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        public string Value { get; set; }
        public DateTime? Date { get; set; }
        [Range(0, long.MaxValue)]
        public long? CreatedById { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }

        public UserFullDto CreatedBy { get; set; }
    }
}
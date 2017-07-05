using Excelsior.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class VisitMatrixProcedureBaseDto
    {
        public VisitMatrixProcedureBaseDto()
            : this(null)
        {
        }
        public VisitMatrixProcedureBaseDto(CERT_ImgProcedureList entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.ImgProcedureID;
                Name = entity.ImgProcedureName;
                Description = entity.ImgProcedureDescription;
                DataTypeId = entity.DataTypeID;
            }
        }
        public virtual CERT_ImgProcedureList ToEntity(CERT_ImgProcedureList entity = null)
        {
            if (entity == null)
            {
                entity = new CERT_ImgProcedureList();
            }

            entity.ImgProcedureID = Id.GetValueOrDefault();
            entity.ImgProcedureName = Name;
            entity.ImgProcedureDescription = Description;
            entity.DataTypeID = DataTypeId;

            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? DataTypeId { get; set; }
        [Range(0, long.MaxValue)]
        public long? TimePointId { get; set; }

        public string Status { get; set; }
        public DateTime? StudyDate { get; set; }
        public int totalQueriesPending { get; set; }
        public int totalQueriesFlagged { get; set; }
    }
}

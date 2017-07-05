using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class CrfDataBaseDto
    {
        public CrfDataBaseDto()
            : this(null)
        {
        }
        public CrfDataBaseDto(CRF_Datum entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CRFDataID;
                SeriesId = entity.SeriesID;
                TemplateId = entity.CRFTemplateID;
                VerifiedById = entity.VerifiedByID;
                SignedById = entity.SignedByID;
                DateVerified = entity.VerifiedDateTime;
                DateSigned = entity.SignedDateTime;
                IsActive = entity.IsActive;

                TimePointId = entity.PACSSeries?.PACSTPProcList.TimePointsListID;
                TimePointName = entity.PACSSeries?.PACSTPProcList.PACSTimePointsList.TimePointsDescription;
                ProcedureId = entity.PACSSeries?.PACSTPProcList.ImgProcedureID;
                ProcedureName = entity.PACSSeries?.PACSTPProcList.CERTImgProcedureList.ImgProcedureName;
            }
        }
        public virtual CRF_Datum ToEntity(CRF_Datum entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CRF_Datum();
            }

            entity.CRFDataID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["seriesid"])
                    entity.SeriesID = SeriesId;
                if (fieldvalidation["templateid"])
                    entity.CRFTemplateID = TemplateId;
                if (fieldvalidation["verifiedbyid"])
                    entity.VerifiedByID = VerifiedById;
                if (fieldvalidation["signedByid"])
                    entity.SignedByID = SignedById;
                if (fieldvalidation["dateverified"])
                    entity.VerifiedDateTime = DateVerified;
                if (fieldvalidation["datesigned"])
                    entity.SignedDateTime = DateSigned;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
            }


            return entity;
        }

        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? TemplateId { get; set; }
        [Range(0, long.MaxValue)]
        public long? VerifiedById { get; set; }
        [Range(0, long.MaxValue)]
        public long? SignedById { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateVerified { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateSigned { get; set; }
        public bool? IsActive { get; set; }
        [Range(0, long.MaxValue)]
        public long? TimePointId { get; set; }
        public string TimePointName { get; set; }
        [Range(0, long.MaxValue)]
        public long? ProcedureId { get; set; }
        public string ProcedureName { get; set; }
    }
}

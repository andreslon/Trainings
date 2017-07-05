using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class EquipmentBaseDto
    {
        public EquipmentBaseDto()
            : this(null)
        {

        }
        public EquipmentBaseDto(CONTACT_Equipment entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.EquipmentID;
                StationName = entity.StationName;
                SoftwareVersion = entity.SoftwareVersion;
                SeconarySerialNum = entity.SeconarySerialNum;
                OtherSerialNum = entity.OtherSerialNum;
                Notes = entity.Notes;
                MainSerialNum = entity.MainSerialNum;
                LastCalibrationDate = entity.LastCalibrationDate;
                LastCalibrationTime = entity.LastCalibrationTime;
                IsValidated = entity.IsValidated;
                IsActive = entity.IsActive;
                FirmwareVersion = entity.FirmwareVersion;
                EquipmentModelId = entity.EquipmentModelID;
                AffiliationId = entity.AffiliationID;
                if (entity.CONTACTEquipmentModel != null)
                {
                    EquipmentModel = new EquipmentModelFullDto(entity.CONTACTEquipmentModel, this);
                }
                if (entity.CONTACTAffiliation != null)
                {
                    Affiliation = new AffiliationFullDto(entity.CONTACTAffiliation, this);
                }
            }
        }
        public virtual CONTACT_Equipment ToEntity(CONTACT_Equipment entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CONTACT_Equipment();
            }

            entity.EquipmentID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["stationname"])
                    entity.StationName = StationName;
                if (fieldvalidation["softwareversion"])
                    entity.SoftwareVersion = SoftwareVersion;
                if (fieldvalidation["seconaryserialnum"])
                    entity.SeconarySerialNum = SeconarySerialNum;
                if (fieldvalidation["otherserialnum"])
                    entity.OtherSerialNum = OtherSerialNum;
                if (fieldvalidation["notes"])
                    entity.Notes = Notes;
                if (fieldvalidation["mainserialnum"])
                    entity.MainSerialNum = MainSerialNum;
                if (fieldvalidation["lastcalibrationdate"])
                    entity.LastCalibrationDate = LastCalibrationDate;
                if (fieldvalidation["lastcalibrationtime"])
                    entity.LastCalibrationTime = LastCalibrationTime;
                if (fieldvalidation["isvalidated"])
                    entity.IsValidated = IsValidated.GetValueOrDefault();
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault();
                if (fieldvalidation["firmwareversion"])
                    entity.FirmwareVersion = FirmwareVersion;
                if (fieldvalidation["equipmentmodelid"])
                    entity.EquipmentModelID = EquipmentModelId;
                if (fieldvalidation["affiliationid"])
                    entity.AffiliationID = AffiliationId; 
            } 
            return entity;
        }

        public long? Id { get; set; }
        [StringLength(50)]
        public string StationName { get; set; }
        [StringLength(50)]
        public string SoftwareVersion { get; set; }
        [StringLength(50)]
        public string SeconarySerialNum { get; set; }
        [StringLength(50)]
        public string OtherSerialNum { get; set; }
        [StringLength(256)]
        public string Notes { get; set; }
        [StringLength(50)]
        public string MainSerialNum { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastCalibrationTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastCalibrationDate { get; set; }
        public bool? IsValidated { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string FirmwareVersion { get; set; }
        [Range(0, long.MaxValue)]
        public long? EquipmentModelId { get; set; }
        [Range(0, long.MaxValue)]
        public long? AffiliationId { get; set; }
        public EquipmentModelFullDto EquipmentModel { get; set; }
        public AffiliationFullDto Affiliation { get; set; }
    }
}

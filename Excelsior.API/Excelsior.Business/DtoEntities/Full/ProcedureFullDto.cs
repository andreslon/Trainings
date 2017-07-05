using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class ProcedureFullDto : ProcedureBaseDto
    {
        public ProcedureFullDto()
            : this(null)
        {

        }
        public ProcedureFullDto(CERT_ImgProcedureList entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                DICOMAcquisitionDeviceCodeSchemeDesignator = entity.DICOMAcquisitionDeviceCodeSchemeDesignator;
                DICOMAcquisitionDeviceCodeValue = entity.DICOMAcquisitionDeviceCodeValue;
                DICOMAcquisitionDeviceCodeMeaning = entity.DICOMAcquisitionDeviceCodeMeaning;
                DICOMAnatomicStructureCodeSchemeDesignator = entity.DICOMAnatomicStructureCodeSchemeDesignator;
                DICOMAnatomicStructureCodeValue = entity.DICOMAnatomicStructureCodeValue;
                DICOMAnatomicStructureCodeMeaning = entity.DICOMAnatomicStructureCodeMeaning;

                if (!(sender is MediaTypeBaseDto) && entity.PACSDataType != null)
                {
                    MediaType = new MediaTypeFullDto(entity.PACSDataType, this);
                }
            }
        }
        public override CERT_ImgProcedureList ToEntity(CERT_ImgProcedureList entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            entity.DICOMAcquisitionDeviceCodeSchemeDesignator = DICOMAcquisitionDeviceCodeSchemeDesignator;
            entity.DICOMAcquisitionDeviceCodeValue = DICOMAcquisitionDeviceCodeValue;
            entity.DICOMAcquisitionDeviceCodeMeaning = DICOMAcquisitionDeviceCodeMeaning;
            entity.DICOMAnatomicStructureCodeSchemeDesignator = DICOMAnatomicStructureCodeSchemeDesignator;
            entity.DICOMAnatomicStructureCodeValue = DICOMAnatomicStructureCodeValue;
            entity.DICOMAnatomicStructureCodeMeaning = DICOMAnatomicStructureCodeMeaning;

            return entity;
        }

        public string DICOMAcquisitionDeviceCodeSchemeDesignator { get; set; }
        public string DICOMAcquisitionDeviceCodeValue { get; set; }
        public string DICOMAcquisitionDeviceCodeMeaning { get; set; }
        public string DICOMAnatomicStructureCodeSchemeDesignator { get; set; }
        public string DICOMAnatomicStructureCodeValue { get; set; }
        public string DICOMAnatomicStructureCodeMeaning { get; set; }
        public MediaTypeFullDto MediaType { get; set; }
    }
}

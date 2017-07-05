using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class ImgProcedureResponseDto
    {
        public long? ImgProcedureID { get; set; }
        public string ImgProcedureName { get; set; }
        public string DICOMAcquisitionDeviceCodeSchemeDesignator { get; set; }
        public string DICOMAcquisitionDeviceCodeValue { get; set; }
        public string DICOMAcquisitionDeviceCodeMeaning { get; set; }
        public string DICOMAnatomicStructureCodeSchemeDesignator { get; set; }
        public string DICOMAnatomicStructureCodeValue { get; set; }
        public string DICOMAnatomicStructureCodeMeaning { get; set; }
        public long? DataTypeID { get; set; }
        public DataTypeResponseDto DataType { get; set; }
    }
}

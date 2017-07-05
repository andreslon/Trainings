using AutoMapper;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Domain;

namespace Excelsior.Business.Helpers
{
    public class RawDataHelper
    {
        public static RawDataResponseDto EntityListRawDataToDto(PACS_RawDatum lst)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PACS_RawDatum, RawDataResponseDto>();
                cfg.CreateMap<PACS_DataType, DataTypeResponseDto>();
                cfg.CreateMap<PACS_RawDataStatus, RDStatusResponseDto>();
                cfg.CreateMap<PACS_DicomOP, DicomOPResponseDto>();
                cfg.CreateMap<PACS_DicomOPT, DicomOPTResponseDto>();
                cfg.CreateMap<PACS_DicomWSI, DicomWSIResponseDto>();
            }); 
            var mapper = config.CreateMapper();
            return (mapper.Map<RawDataResponseDto>(lst));
        }
        public static RawDataResponseDto EntityRawDataToDto(PACS_RawDatum ent)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PACS_RawDatum, RawDataResponseDto>();
                cfg.CreateMap<PACS_DataType, DataTypeResponseDto>();
                cfg.CreateMap<PACS_RawDataStatus, RDStatusResponseDto>();
                cfg.CreateMap<PACS_DicomOP, DicomOPResponseDto>();
                cfg.CreateMap<PACS_DicomOPT, DicomOPTResponseDto>();
                cfg.CreateMap<PACS_DicomWSI, DicomWSIResponseDto>();
            });
            var mapper = config.CreateMapper();
            return (mapper.Map<RawDataResponseDto>(ent));
        }
    }
}

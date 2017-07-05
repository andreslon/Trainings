using AutoMapper;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Domain;

namespace Excelsior.Business.Helpers
{
    public class ProcessedDataHelper
    {
         
        public static  PACS_ProcessedDatum DtoProcessedDataToEntity(ProcessedDataRequestDto ent)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProcessedDataRequestDto, PACS_ProcessedDatum>();
            });
            var mapper = config.CreateMapper();
            return (mapper.Map<PACS_ProcessedDatum>(ent));
        }

        public static ProcessedDataResponseDto EntityToDtoProcessedData(PACS_ProcessedDatum ent)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PACS_ProcessedDatum, ProcessedDataResponseDto>();
            });
            var mapper = config.CreateMapper();
            return (mapper.Map<ProcessedDataResponseDto>(ent));
        }
    }
}

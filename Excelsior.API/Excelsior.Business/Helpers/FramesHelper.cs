using AutoMapper;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.Helpers
{
    public class FramesHelper
    {
        public static List<FramesResponseDto> EntityListFrameToDto(List<PACS_DicomFrame> lst)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PACS_DicomFrame, FramesResponseDto>();
            }); 
            var mapper = config.CreateMapper();
            return (mapper.Map<List<FramesResponseDto>>(lst));
        }
        public static FramesResponseDto EntityFrameToDto(PACS_DicomFrame ent)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PACS_DicomFrame, FramesResponseDto>();
            });
            var mapper = config.CreateMapper();
            return (mapper.Map<FramesResponseDto>(ent));
        }
    }
}

using AutoMapper;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.Helpers
{
    public static class TrialHelper
    {
        public static List<TrialsResponseDto> EntityTrialToTrialDto(List<PACS_Trial> lst)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PACS_Trial, TrialsResponseDto>();
            });
            var mapper = config.CreateMapper();
            return (mapper.Map<List<TrialsResponseDto>>(lst));
        }
    }
}

using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Business.Handlers;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.API.Repositories
{
    public class ProcessedDataRepository
    {
        public DataModel db { get; set; }
        public ProcessedDataRepository(DataModel context)
        {
            db = context;
        }

        public bool SetProcessedData(ProcessedDataRequestDto dto)
        {

            ProcessedDataHandler handler = new ProcessedDataHandler(db);
            var entity = ProcessedDataHelper.DtoProcessedDataToEntity(dto);
            var result = handler.SetProcessedData(entity);
            return result;
        }

        public List<ProcessedDataResponseDto> GetProcessedData(long rawDataID)
        {
            ProcessedDataHandler handler = new ProcessedDataHandler(db);

            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return handler.GetProcessedData(rawDataID).ToList();
            });

            var list = new List<ProcessedDataResponseDto>();

            foreach (var item in result)
            {
                list.Add(ProcessedDataHelper.EntityToDtoProcessedData(item));
            }

            return list;
        }
    }
}

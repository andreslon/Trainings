using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class MediaStatusGateway : IMediaStatusGateway
    {
        public IMediaStatusRepository Repository { get; set; }
        public MediaStatusGateway(IMediaStatusRepository repository)
        {
            Repository = repository;
        }

        public ResultInfo<IList<MediaStatusBaseDto>> GetAll(BaseRequestDto request)
        {
            var result = new ResultInfo<IList<MediaStatusBaseDto>>();
            try
            {
                var respose = new List<MediaStatusBaseDto>();
                var entities = Repository.GetAll(request.Search);
                var count = 0;
                try
                {
                    count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return entities.Count();
                    });
                }
                catch (Exception e)
                {
                }

                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x => x.StatusID), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new MediaStatusBaseDto(entity);
                        respose.Add(dto);
                    }
                }

                result.Result = respose;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }
    }
}

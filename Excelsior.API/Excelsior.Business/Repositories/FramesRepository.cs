using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Business.Helpers;
using Excelsior.Business.Logic;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.API.Repositories
{
    public class FramesRepository
    {
        public DataModel db { get; set; }
        public FramesRepository(DataModel context)
        {
            db = context;
        }

        public ResultInfo<IList<FramesResponseDto>> GetFrames(FramesRequestDto dto)
        {
            var result = new ResultInfo<IList<FramesResponseDto>>();
            try
            {
                var listDto = new List<FramesResponseDto>();
                var handler = new FramesHandler(db);
                var listResult = handler.GetFrames(dto.RawDataId);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return listResult.Count();
                });
                result.SetPager(count, dto.Page, dto.PageSize);
                var listPaged = GeneralHelper.GetPagedList(listResult.OrderBy(x => x.FrameIndex), result.Pager);
                if (listPaged != null)
                {
                    listDto = FramesHelper.EntityListFrameToDto(listPaged);
                }

                result.Result = listDto;
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

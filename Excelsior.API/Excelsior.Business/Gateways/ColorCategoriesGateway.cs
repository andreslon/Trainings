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
    public class ColorCategoriesGateway : IColorCategoriesGateway
    {
        public IColorCategoriesRepository Repository { get; set; }
        public ColorCategoriesGateway(IColorCategoriesRepository repository)
        {
            Repository = repository;
        }

        public ResultInfo<IList<ColorCategoryBaseDto>> GetAll(ColorCategoriesRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<ColorCategoryBaseDto>>();
            try
            {
                var respose = new List<ColorCategoryBaseDto>();
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
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x => x.CategoryDes), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new ColorCategoryBaseDto(entity);
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

        public ResultInfo<ColorCategoryFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<ColorCategoryFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CategoryFlagID == id);
                if (entity != null)
                {
                    var dto = new ColorCategoryFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Color Category not found");
                }
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

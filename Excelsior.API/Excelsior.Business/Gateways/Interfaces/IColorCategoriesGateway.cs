﻿using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface IColorCategoriesGateway
    {
        ResultInfo<IList<ColorCategoryBaseDto>> GetAll(ColorCategoriesRequestDto request);
        ResultInfo<ColorCategoryFullDto> GetSingle(long id);
    }    
}
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.Gateways.Interfaces;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.Gateways
{
    public class CommonGateway: ICommonGateway
    {
        public ICountriesRepository Repository { get; set; }
        public IStringResources Resources { get; set; }
        public CommonGateway(ICountriesRepository countriesRepository, IStringResources resources) {
            Repository = countriesRepository;
            Resources = resources;
        }
        public ResultInfo<IList<CountryBaseDto>> GetCountries()
        { 
            var result = new ResultInfo<IList<CountryBaseDto>>();
            try
            {
                var countriesResult = new List<CountryBaseDto>();
                var countries = Repository.GetAll();
                if (countries != null)
                {
                    foreach (var country in countries)
                    {
                        var dto = new CountryBaseDto(country);
                        countriesResult.Add(dto);
                    }
                } 
                result.Result = countriesResult;
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
        public ResultInfo<IList<string>> GetSecurityQuestions()
        {
            var result = new ResultInfo<IList<string>>();
            try
            {
                result.Result = Resources.GetSecurityQuestions();
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

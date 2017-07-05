using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.Gateways.Interfaces
{
    public interface ICommonGateway
    {
        ResultInfo<IList<CountryBaseDto>> GetCountries();
        ResultInfo<IList<string>> GetSecurityQuestions();
    }
}

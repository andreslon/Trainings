using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System;

namespace Excelsior.Business.Gateways
{
    public interface IAccountGateway
    {
        ResultInfo<bool> ChangePassword(AccountFullDto request);
        ResultInfo<bool> ChangePin(AccountFullDto request);
        ResultInfo<bool> ChangeSecurityQuestion(AccountFullDto request);
    }
}

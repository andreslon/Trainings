using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System;

namespace Excelsior.Business.Gateways
{
    public interface IUsersGateway : IBaseGateway<UserFullDto, UserBaseDto, UsersRequestDto>
    {
        ResultInfo<UserFullDto> GetSingle(Guid id);
        ResultInfo<bool> SendEmail(EmailBaseDto email);
        ResultInfo<string> DecryptUserId(string cryptedcode);
        ResultInfo<bool> Registration(RegistrationFullDto request, string fields = null);
    }
}
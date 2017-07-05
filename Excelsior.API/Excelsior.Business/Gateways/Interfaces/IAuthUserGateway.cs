using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System;

namespace Excelsior.Business.Gateways
{
    public interface IAuthUserGateway : IBaseGateway<AuthUserFullDto, AuthUserBaseDto, AuthUsersRequestDto>
    {
        ResultInfo<AuthUserFullDto> GetByUserName(string userName);
        ResultInfo<AuthRecoveryDataBaseDto> GetRecoveryDataByUserName(string userName);
        ResultInfo<bool> RecoveryDataByEmail(string email);
        ResultInfo<bool> ForgotAnswer(string username);
        ResultInfo<AuthUserFullDto> GetSingle(Guid id);
        ResultInfo<bool> Delete(Guid id);
        ResultInfo<bool> ResetPassword(AuthRecoveryDataFullDto request);
        ResultInfo<bool> ChangePassword(AuthRecoveryDataFullDto request);
        ResultInfo<string> DecryptUserName(string cryptedcode);
        ResultInfo<bool> Logout();
    }
}

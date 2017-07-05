using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways.Interfaces
{
    public interface ICertUsersGateway : IBaseGateway<CertUserFullDto, CertUserBaseDto, CertUserRequestDto>
    {
        ResultInfo<CertUserBaseDto> Assign(long id);
        ResultInfo<CertUserBaseDto> Certify(long id, string password);
        ResultInfo<CertUserBaseDto> Reject(long id, string password, string reason);
        ResultInfo<IList<CertUserBaseDto>> GetPrevCertifications(long id, BaseRequestDto request);
    }
}
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways.Interfaces
{
    public interface ICertEquipmentGateway : IBaseGateway<CertEquipmentFullDto, CertEquipmentBaseDto, CertEquipmentRequestDto>
    {
        ResultInfo<CertEquipmentBaseDto> Assign(long id);
        ResultInfo<CertEquipmentBaseDto> Certify(long id, CertifyEquipmentRequestDto request, string password);
        ResultInfo<CertEquipmentBaseDto> Reject(long id, RejectCertificationRequestDto request, string password, string reason);
        ResultInfo<IList<CertEquipmentBaseDto>> GetPrevCertifications(long id, BaseRequestDto request);
    }
}

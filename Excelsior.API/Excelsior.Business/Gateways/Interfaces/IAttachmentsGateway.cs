using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;
using System;

namespace Excelsior.Business.Gateways
{
    public interface IAttachmentsGateway
    {
        ResultInfo<IList<AttachmentBaseDto>> GetAll(AttachementsRequestDto request);
        ResultInfo<AttachmentFullDto> GetSingle(long id);
        ResultInfo<AttachmentFullDto> Add(AttachmentFullDto request, string userId);
        ResultInfo<AttachmentFullDto> Update(AttachmentFullDto request);
        ResultInfo<bool> Delete(long id);
    }
}
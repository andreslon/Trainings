using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System;

namespace Excelsior.Business.Gateways
{
    public interface IDocumentVersionsGateway : IBaseGateway<DocumentVersionFullDto, DocumentVersionBaseDto, DocumentVersionsRequestDto>
    {
        ResultInfo<DocumentVersionFullDto> GetSingleAttachment(long id);
        ResultInfo<DocumentVersionFullDto> UpdateAttachment(DocumentVersionFullDto request, string fields = null, string password = null, string reason = null);
    }
}
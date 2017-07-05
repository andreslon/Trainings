using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System;

namespace Excelsior.Business.Gateways
{
    public interface IDocumentRolesGateway : IBaseGateway<DocumentRoleFullDto, DocumentRoleBaseDto, DocumentRolesRequestDto>
    {
        
    }
}
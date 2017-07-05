using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.Helpers;
using Excelsior.Domain.Repositories;
using System;

namespace Excelsior.Business.Gateways
{
    public class EDCGateway : IEDCGateway
    {
        public IVisitMatrixRepository VisitMatrixRepository { get; set; }
        public IGradingTemplatesRepository GradingTemplatesRepository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }

        public EDCGateway(IVisitMatrixRepository vmRepository, IGradingTemplatesRepository gtRepository, IAuditRecordsRepository auditRecordsRepository)
        {
            VisitMatrixRepository = vmRepository;
            GradingTemplatesRepository = gtRepository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<string> GetFrameLocation(long subjectId, long timePointListId, long procedureId)
        {
            var result = new ResultInfo<string>();
            try
            {
                result.Result = VisitMatrixRepository.GetFrameLocation(subjectId, timePointListId, procedureId);
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

        public ResultInfo<GradingTemplateFullDto> GetGradingTemplateForProcedure(long procedureId, long timePointId, bool isHierarchical)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<GradingTemplateFullDto>();
            try
            {
                var template = GradingTemplatesRepository.GetGradingTemplateForProcedure(procedureId, timePointId);
                if (template != null)
                {
                    var templateResult = new GradingTemplateFullDto(template);

                    GradingHelper.fillTemplateResult(GradingTemplatesRepository, template, templateResult, isHierarchical);

                    result.Result = templateResult;
                }
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

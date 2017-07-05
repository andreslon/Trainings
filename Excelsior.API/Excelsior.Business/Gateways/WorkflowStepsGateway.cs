using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Domain;
using Excelsior.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class WorkflowStepsGateway : IWorkflowStepsGateway
    {
        public IWorkflowStepsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public WorkflowStepsGateway(IWorkflowStepsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<WorkflowStepBaseDto>> GetAll(WorkflowStepsRequestDto request)
        {
            var result = new ResultInfo<IList<WorkflowStepBaseDto>>();
            try
            {
                var wfStepsResponse = new List<WorkflowStepBaseDto>();

                IQueryable<WF_StepList> wfSteps = Repository.GetAll();
                foreach (var step in wfSteps.OrderBy(item => item.SortingOrder))
                {
                    var dto = new WorkflowStepBaseDto(step);
                    wfStepsResponse.Add(dto);
                }

                result.Result = wfStepsResponse;
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

        public ResultInfo<WorkflowStepFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<WorkflowStepFullDto>();

            return result;
        }

        public ResultInfo<WorkflowStepFullDto> Add(WorkflowStepFullDto request)
        {
            var result = new ResultInfo<WorkflowStepFullDto>();
            //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
            return result;
        }

        public ResultInfo<WorkflowStepFullDto> Update(WorkflowStepFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<WorkflowStepFullDto>();
            //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
            return result;
        }

        public ResultInfo<bool> Delete(long id)
        {
            var result = new ResultInfo<bool>();
            //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
            return result;
        }
    }
}

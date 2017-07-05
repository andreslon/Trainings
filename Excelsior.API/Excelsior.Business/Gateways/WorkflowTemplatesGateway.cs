using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class WorkflowTemplatesGateway : IWorkflowTemplatesGateway
    {
        public IWorkflowTemplatesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }

        public WorkflowTemplatesGateway(IWorkflowTemplatesRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<WorkflowTemplateBaseDto>> GetAll(WorkflowTemplatesRequestDto request)
        {
            var result = new ResultInfo<IList<WorkflowTemplateBaseDto>>();
            try
            {
                var templatesResult = new List<WorkflowTemplateBaseDto>();

                IQueryable<WF_Template> templates = Repository.GetAll(request.StudyId, request.IsActive, request.IsLocked, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return templates.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var templatesPaged = GeneralHelper.GetPagedList(templates.OrderBy(x => x.WFTemplateName), result.Pager);
                if (templatesPaged != null)
                {
                    foreach (var entity in templatesPaged)
                    {
                        var dto = new WorkflowTemplateBaseDto(entity);
                        templatesResult.Add(dto);
                    }
                }

                result.Result = templatesResult;
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

        public ResultInfo<WorkflowTemplateFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<WorkflowTemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.WFTemplateID == id);
                if (entity != null)
                {
                    var dto = new WorkflowTemplateFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Workflow Template not found");
                }
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

        public ResultInfo<WorkflowTemplateFullDto> Add(WorkflowTemplateFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<WorkflowTemplateFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                if (request.Steps != null)
                    AttachSteps(entity, request.Steps);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new WorkflowTemplateFullDto(entity);
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

        public ResultInfo<WorkflowTemplateFullDto> Update(WorkflowTemplateFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<WorkflowTemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.WFTemplateID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new WorkflowTemplateFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Workflow Template not found");
                }
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

        public ResultInfo<bool> Delete(long id)
        {
            //Perform input validation
            //---- 
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.WFTemplateID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Workflow Template not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<IList<WorkflowTemplateStepFullDto>> GetSteps(long id)
        {
            var result = new ResultInfo<IList<WorkflowTemplateStepFullDto>>();
            try
            {
                var entity = Repository.GetSingle(x => x.WFTemplateID == id);
                if (entity != null)
                {
                    var entities = Repository.GetSteps(entity);
                    result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return entities.Select(x => new WorkflowTemplateStepFullDto(x)).ToList();
                    });
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Workflow Template not found");
                }
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

        public ResultInfo<WorkflowTemplateFullDto> SetSteps(long id, IList<WorkflowTemplateStepFullDto> steps)
        {
            var result = new ResultInfo<WorkflowTemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.WFTemplateID == id);
                if (entity != null)
                {
                    AttachSteps(entity, steps);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new WorkflowTemplateFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Workflow Template not found");
                }
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

        private void AttachSteps(WF_Template entity, IList<WorkflowTemplateStepFullDto> steps)
        {
            var stepEntities = RefreshOrRemoveExistingSteps(entity, steps);
            for (var i = 0; i < steps.Count; i++)
            {
                var groupEntity = Repository.AddStep(entity, stepEntities[i]);
            }
        }

        private IList<WF_TempStep> RefreshOrRemoveExistingSteps(WF_Template entity, IList<WorkflowTemplateStepFullDto> steps)
        {
            var entitySteps = entity.WF_TempSteps.ToList();
            foreach (var step in entitySteps)
            {
                if (!steps.Any(x => x.Id == step.WFTempStepID))
                    Repository.Context.Delete(step);
            }

            var stepEntities = new List<WF_TempStep>();
            foreach (var step in steps)
            {
                var entityStep = entitySteps.FirstOrDefault(x => x.WFTempStepID == step.Id);
                if (entityStep != null)
                {
                    stepEntities.Add(step.ToEntity(entityStep));
                }
                else
                {
                    stepEntities.Add(step.ToEntity());
                }
            }

            return stepEntities;
        }

        public ResultInfo<WorkflowTemplateFullDto> Clone(long id, CommonRequestDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<WorkflowTemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.WFTemplateID == id);
                if (entity != null)
                {
                    var cloneEntity = Repository.Clone(entity, request.Id);
                    Repository.Commit();
                    Repository.Refresh(cloneEntity);
                    var dto = new WorkflowTemplateFullDto(cloneEntity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Workflow Template not found");
                }
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

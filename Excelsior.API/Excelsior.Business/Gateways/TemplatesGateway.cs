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
    public class TemplatesGateway : ITemplatesGateway
    {
        public ITemplatesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public TemplatesGateway(ITemplatesRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<TemplateBaseDto>> GetAll(TemplatesRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<TemplateBaseDto>>();
            try
            {
                var studyTemplatesRespose = new List<TemplateBaseDto>();
                var studyTemplates = Repository.GetAll(request.StudyId, request.TimePointId, request.ProcedureId, request.IsActive, request.IsLocked, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return studyTemplates.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var studyTemplatesPaged = GeneralHelper.GetPagedList(studyTemplates.OrderBy(x => x.TemplateName), result.Pager);
                if (studyTemplatesPaged != null)
                {
                    foreach (var entity in studyTemplatesPaged)
                    {
                        var dto = new TemplateFullDto(entity);
                        studyTemplatesRespose.Add(dto);
                    }
                }

                result.Result = studyTemplatesRespose;
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

        public ResultInfo<TemplateFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateID == id);
                if (entity != null)
                {
                    var dto = new TemplateFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template not found");
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

        public ResultInfo<TemplateFullDto> Add(TemplateFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                if(request.Groups != null)
                    AttachGroups(entity, request.Groups);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new TemplateFullDto(entity);
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

        public ResultInfo<TemplateFullDto> Update(TemplateFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<TemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template not found");
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
                var entity = Repository.GetSingle(x => x.CRFTemplateID == id);
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
                    throw new Exception("Template not found");
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

        public ResultInfo<IList<TemplateGroupFullDto>> GetGroups(long id)
        {
            var result = new ResultInfo<IList<TemplateGroupFullDto>>();
            try
            {
                var entities = Repository.GetGroups(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new TemplateGroupFullDto(x, new TemplateBaseDto())).ToList();
                }); 
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

        public ResultInfo<TemplateFullDto> SetGroups(long id, IList<TemplateGroupFullDto> groups)
        {
            var result = new ResultInfo<TemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateID == id);
                if (entity != null)
                {
                    AttachGroups(entity, groups);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template not found");
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

        private void AttachGroups(CRF_Template entity, IList<TemplateGroupFullDto> groups)
        {
            var groupEntities = RefreshOrRemoveExistingGroups(entity, groups);
            for (var i = 0; i < groups.Count; i++)
            {
                var groupEntity = Repository.AddGroup(entity, groupEntities[i]);
            }
        }

        private IList<CRF_TemplateGroup> RefreshOrRemoveExistingGroups(CRF_Template entity, IList<TemplateGroupFullDto> groups)
        {
            var entityGroups = entity.CRF_TemplateGroups.ToList();
            foreach (var group in entityGroups)
            {
                if (!groups.Any(x => x.Id == group.CRFTemplateGroupID))
                    Repository.Context.Delete(group);
            }

            var groupEntities = new List<CRF_TemplateGroup>();
            foreach (var group in groups)
            {
                var entityGroup = entityGroups.FirstOrDefault(x => x.CRFTemplateGroupID == group.Id);
                if (entityGroup != null)
                {
                    groupEntities.Add(group.ToEntity(entityGroup));
                }
                else
                {
                    groupEntities.Add(group.ToEntity());
                }
            }

            return groupEntities;
        }

        public ResultInfo<TemplateFullDto> Clone(long id, CommonRequestDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateID == id);
                if (entity != null)
                {
                    var cloneEntity = Repository.Clone(entity, request.Id);
                    Repository.Commit();
                    Repository.Refresh(cloneEntity);
                    var dto = new TemplateFullDto(cloneEntity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template not found");
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

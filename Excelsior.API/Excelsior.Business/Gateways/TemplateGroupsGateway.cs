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
    public class TemplateGroupsGateway : ITemplateGroupsGateway
    {
        public ITemplateGroupsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public TemplateGroupsGateway(ITemplateGroupsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<TemplateGroupBaseDto>> GetAll(TemplateGroupsRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<TemplateGroupBaseDto>>();
            try
            {
                var groupsRespose = new List<TemplateGroupBaseDto>();
                var groups = Repository.GetAll(request.Search);
                var count = 0;
                try
                {
                    count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return groups.Count();
                    });
                }
                catch (Exception e)
                {
                }

                result.SetPager(count, request.Page, request.PageSize);
                var groupsPaged = GeneralHelper.GetPagedList(groups.OrderBy(x => x.GroupSeq), result.Pager);
                if (groupsPaged != null)
                {
                    foreach (var entity in groupsPaged)
                    {
                        var dto = new TemplateGroupFullDto(entity);
                        groupsRespose.Add(dto);
                    }
                }

                result.Result = groupsRespose;
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

        public ResultInfo<TemplateGroupFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateGroupFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateGroupID == id);
                if (entity != null)
                {
                    var dto = new TemplateGroupFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template Group not found");
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

        public ResultInfo<TemplateGroupFullDto> Add(TemplateGroupFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateGroupFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                if (request.Questions != null)
                    AttachQuestions(entity, request.Questions);
                if (request.Dependencies != null)
                    AttachDependencies(entity, request.Dependencies);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new TemplateGroupFullDto(entity);
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

        public ResultInfo<TemplateGroupFullDto> Update(TemplateGroupFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<TemplateGroupFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateGroupID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateGroupFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template Group not found");
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
                var entity = Repository.GetSingle(x => x.CRFTemplateGroupID == id);
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
                    throw new Exception("Template Group not found");
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

        public ResultInfo<IList<TemplateQuestionFullDto>> GetQuestions(long id)
        {
            var result = new ResultInfo<IList<TemplateQuestionFullDto>>();
            try
            {
                var entities = Repository.GetQuestions(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new TemplateQuestionFullDto(x, new TemplateGroupBaseDto())).ToList();
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

        public ResultInfo<TemplateGroupFullDto> SetQuestions(long id, IList<TemplateQuestionFullDto> questions)
        {
            var result = new ResultInfo<TemplateGroupFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateGroupID == id);
                if (entity != null)
                {
                    AttachQuestions(entity, questions);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateGroupFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template Group not found");
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

        private void AttachQuestions(CRF_TemplateGroup entity, IList<TemplateQuestionFullDto> questions)
        {
            var qEntities = RefreshOrRemoveExistingQuestions(entity, questions);
            for (var i = 0; i < questions.Count; i++)
            {
                var depEntity = Repository.AddQuestion(entity, qEntities[i]);
            }
        }

        private IList<CRF_TemplateQuestion> RefreshOrRemoveExistingQuestions(CRF_TemplateGroup entity, IList<TemplateQuestionFullDto> questions)
        {
            var entityQuestions = entity.CRF_TemplateQuestions.ToList();
            foreach (var question in entityQuestions)
            {
                if (!questions.Any(x => x.Id == question.CRFTemplateQuestionID))
                    Repository.Context.Delete(question);
            }

            var resEntities = new List<CRF_TemplateQuestion>();
            foreach (var question in questions)
            {
                var entitySource = entityQuestions.FirstOrDefault(x => x.CRFTemplateQuestionID == question.Id);
                if (entitySource != null)
                {
                    resEntities.Add(question.ToEntity(entitySource));
                }
                else
                {
                    resEntities.Add(question.ToEntity());
                }
            }

            return resEntities;
        }

        public ResultInfo<IList<TemplateDependencyFullDto>> GetDependencies(long id)
        {
            var result = new ResultInfo<IList<TemplateDependencyFullDto>>();
            try
            {
                var entities = Repository.GetDependencies(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new TemplateDependencyFullDto(x, new TemplateGroupBaseDto())).ToList();
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

        public ResultInfo<TemplateGroupFullDto> SetDependencies(long id, IList<TemplateDependencyFullDto> dependencies)
        {
            var result = new ResultInfo<TemplateGroupFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateGroupID == id);
                if (entity != null)
                {
                    AttachDependencies(entity, dependencies);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateGroupFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template Group not found");
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

        private void AttachDependencies(CRF_TemplateGroup entity, IList<TemplateDependencyFullDto> dependencies)
        {
            var depEntities = RefreshOrRemoveExistingDependencies(entity, dependencies);
            for (var i = 0; i < dependencies.Count; i++)
            {
                var depEntity = Repository.AddDependency(entity, depEntities[i]);
                AttachDependencySources(depEntity, dependencies[i].Sources);
            }
        }

        private IList<CRF_TemplateDependency> RefreshOrRemoveExistingDependencies(CRF_TemplateGroup entity, IList<TemplateDependencyFullDto> dependencies)
        {
            var entityDependencies = entity.CRF_TemplateDependencies.ToList();
            foreach (var dependency in entityDependencies)
            {
                if (!dependencies.Any(x => x.Id == dependency.CRFTemplateDependencyID))
                    Repository.Context.Delete(dependency);
            }

            var resEntities = new List<CRF_TemplateDependency>();
            foreach (var dependency in dependencies)
            {
                var entityResult = entityDependencies.FirstOrDefault(x => x.CRFTemplateDependencyID == dependency.Id);
                if (entityResult != null)
                {
                    resEntities.Add(dependency.ToEntity(entityResult));
                }
                else
                {
                    resEntities.Add(dependency.ToEntity());
                }
            }

            return resEntities;
        }

        private void AttachDependencySources(CRF_TemplateDependency entity, IList<TemplateDependencySourceFullDto> sources)
        {
            var ansEntities = RefreshOrRemoveExistingSources(entity, sources);
            for (var i = 0; i < sources.Count; i++)
            {
                Repository.AddDependencySource(entity, ansEntities[i]);
            }
        }

        private IList<CRF_TemplateDependencySource> RefreshOrRemoveExistingSources(CRF_TemplateDependency entity, IList<TemplateDependencySourceFullDto> sources)
        {
            var entitySources = entity.CRF_TemplateDependencySources.ToList();
            foreach (var source in entitySources)
            {
                if (!sources.Any(x => x.Id == source.CRFTemplateDependencyID))
                    Repository.Context.Delete(source);
            }

            var sourceEntities = new List<CRF_TemplateDependencySource>();
            foreach (var source in sources)
            {
                var entitySource = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.Context.CRF_TemplateDependencySources.FirstOrDefault(x => x.CRFTemplateDependencySourceID == source.Id);
                });
                if (entitySource != null)
                {
                    sourceEntities.Add(source.ToEntity(entitySource));
                }
                else
                {
                    sourceEntities.Add(source.ToEntity());
                }
            }

            return sourceEntities;
        }
    }
}

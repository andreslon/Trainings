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
    public class TemplateDependenciesGateway : ITemplateDependenciesGateway
    {
        public ITemplateDependenciesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public TemplateDependenciesGateway(ITemplateDependenciesRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<TemplateDependencyBaseDto>> GetAll(TemplateDependenciesRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<TemplateDependencyBaseDto>>();
            try
            {
                var studyDependenciesRespose = new List<TemplateDependencyBaseDto>();
                var studyDependencies = Repository.GetAll(request.SourceAnswerId, request.ActionEnable);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return studyDependencies.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var dependenciesPaged = GeneralHelper.GetPagedList(studyDependencies.OrderBy(x => x.CRFTemplateDependencyID), result.Pager);
                if (dependenciesPaged != null)
                {
                    foreach (var entity in dependenciesPaged)
                    {
                        var dto = new TemplateDependencyFullDto(entity);
                        studyDependenciesRespose.Add(dto);
                    }
                }

                result.Result = studyDependenciesRespose;
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

        public ResultInfo<TemplateDependencyFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateDependencyFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateDependencyID == id);
                if (entity != null)
                {
                    var dto = new TemplateDependencyFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Dependency not found");
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

        public ResultInfo<TemplateDependencyFullDto> Add(TemplateDependencyFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateDependencyFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                if (request.Sources != null)
                    AttachSources(entity, request.Sources);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new TemplateDependencyFullDto(entity);
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

        public ResultInfo<TemplateDependencyFullDto> Update(TemplateDependencyFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<TemplateDependencyFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateDependencyID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateDependencyFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Dependency not found");
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
                var entity = Repository.GetSingle(x => x.CRFTemplateDependencyID == id);
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
                    throw new Exception("Dependency not found");
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

        public ResultInfo<IList<TemplateDependencySourceFullDto>> GetSources(long id)
        {
            var result = new ResultInfo<IList<TemplateDependencySourceFullDto>>();
            try
            {
                var entities = Repository.GetSources(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new TemplateDependencySourceFullDto(x, new TemplateDependencyBaseDto())).ToList();
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

        public ResultInfo<TemplateDependencyFullDto> SetSources(long id, IList<TemplateDependencySourceFullDto> sources)
        {
            var result = new ResultInfo<TemplateDependencyFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateDependencyID == id);
                if (entity != null)
                {
                    AttachSources(entity, sources);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateDependencyFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Dependency not found");
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

        private void AttachSources(CRF_TemplateDependency entity, IList<TemplateDependencySourceFullDto> sources)
        {
            var sourceEntities = RefreshOrRemoveExistingSources(entity, sources);
            for (var i = 0; i < sources.Count; i++)
            {
                var sourceEntity = Repository.AddSource(entity, sourceEntities[i]);
            }
        }

        private IList<CRF_TemplateDependencySource> RefreshOrRemoveExistingSources(CRF_TemplateDependency entity, IList<TemplateDependencySourceFullDto> sources)
        {
            var entitySources = entity.CRF_TemplateDependencySources.ToList();
            foreach (var source in entitySources)
            {
                if (!sources.Any(x => x.Id == source.CRFTemplateDependencySourceID))
                    Repository.Context.Delete(source);
            }

            var sourceEntities = new List<CRF_TemplateDependencySource>();
            foreach (var source in sources)
            {
                var entitySource = entitySources.FirstOrDefault(x => x.CRFTemplateDependencySourceID == source.Id);
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

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
    public class CrfDataGateway : ICrfDataGateway
    {
        public ICrfDataRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public ISeriesGateway SeriesGateway { get; set; }
        public CrfDataGateway(ICrfDataRepository repository, IAuditRecordsRepository auditRecordsRepository, ISeriesGateway seriesGateway)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            SeriesGateway = seriesGateway;
        } 

        public ResultInfo<IList<CrfDataBaseDto>> GetAll(CrfDataRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<CrfDataBaseDto>>();
            try
            {
                var response = new List<CrfDataBaseDto>();
                var entities = Repository.GetAll(request.SeriesId, request.SubjectId, request.TimePointId, request.ProcedureId, request.IsActive);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderByDescending(x => x.SignedDateTime), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new CrfDataBaseDto(entity);
                        response.Add(dto);
                    }
                }

                result.Result = response;
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

        public ResultInfo<CrfDataFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<CrfDataFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFDataID == id);
                if (entity != null)
                {
                    var dto = new CrfDataFullDto(entity);
                    if(entity.SeriesID.HasValue)
                        dto.Series = SeriesGateway.GetSingle(entity.SeriesID.Value).Result;
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Crf Data not found");
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

        public ResultInfo<CrfDataFullDto> Add(CrfDataFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<CrfDataFullDto>();
            try
            {
                if(request.SeriesId == null && request.Series != null)
                {
                    var series = SeriesGateway.Add(request.Series);
                    if (!series.IsSuccess)
                        throw new Exception(series.Exception);

                    request.SeriesId = series.Result.Id;
                }

                var entity = request.ToEntity();
                Repository.Add(entity);
                if (request.Results != null)
                    AttachResults(entity, request.Results);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new CrfDataFullDto(entity);
                if (entity.SeriesID.HasValue)
                    dto.Series = SeriesGateway.GetSingle(entity.SeriesID.Value).Result;
                result.Result = dto;
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

        public ResultInfo<CrfDataFullDto> Update(CrfDataFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<CrfDataFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFDataID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new CrfDataFullDto(entity);
                    if (entity.SeriesID.HasValue)
                        dto.Series = SeriesGateway.GetSingle(entity.SeriesID.Value).Result;
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Crf Data not found");
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
                var entity = Repository.GetSingle(x => x.CRFDataID == id);
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
                    throw new Exception("Crf Data not found");
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

        public ResultInfo<IList<CrfDataResultFullDto>> GetResults(long id)
        {
            var result = new ResultInfo<IList<CrfDataResultFullDto>>();
            try
            {
                var entities = Repository.GetResults(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new CrfDataResultFullDto(x, new CrfDataFullDto())).ToList();
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

        public ResultInfo<CrfDataFullDto> SetResults(long id, IList<CrfDataResultFullDto> results)
        {
            var result = new ResultInfo<CrfDataFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFDataID == id);
                if (entity != null)
                {
                    AttachResults(entity, results);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new CrfDataFullDto(entity);
                    if (entity.SeriesID.HasValue)
                        dto.Series = SeriesGateway.GetSingle(entity.SeriesID.Value).Result;
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Crf Data not found");
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

        private void AttachResults(CRF_Datum entity, IList<CrfDataResultFullDto> results)
        {
            var resEntities = RefreshOrRemoveExistingResults(entity, results);
            for (var i = 0; i < results.Count; i++)
            {
                var resEntity = Repository.AddResult(entity, resEntities[i]);
                AttachResultAnswers(resEntity, results[i].Answers);
            }
        }

        private IList<CRF_DataResult> RefreshOrRemoveExistingResults(CRF_Datum entity, IList<CrfDataResultFullDto> results)
        {
            var entityResults = entity.CRF_DataResults.ToList();
            foreach (var result in entityResults)
            {
                if (!results.Any(x => x.Id == result.CRFDataResultID))
                    Repository.Context.Delete(result);
            }

            var resEntities = new List<CRF_DataResult>();
            foreach (var result in results)
            {
                var entityResult = entityResults.FirstOrDefault(x => x.CRFDataResultID == result.Id);
                if(entityResult != null)
                {
                    resEntities.Add(result.ToEntity(entityResult));
                }
                else
                {
                    resEntities.Add(result.ToEntity());
                }
            }
            return resEntities;
        }

        private void AttachResultAnswers(CRF_DataResult entity, IList<TemplateAnswerBaseDto> answers)
        {
            var ansEntities = RefreshOrRemoveExistingAnswers(entity, answers);
            for (var i = 0; i < answers.Count; i++)
            {
                Repository.AddResultAnswer(entity, ansEntities[i]);
            }
        }

        private IList<CRF_TemplateAnswer> RefreshOrRemoveExistingAnswers(CRF_DataResult entity, IList<TemplateAnswerBaseDto> answers)
        {
            var entityAnswers = entity.CRF_TemplateAnswers.ToList();
            foreach (var answer in entityAnswers)
            {
                if (!answers.Any(x => x.Id == answer.CRFTemplateAnswerID))
                    entity.CRF_TemplateAnswers.Remove(answer);
            }

            var ansEntities = new List<CRF_TemplateAnswer>();
            foreach (var answer in answers)
            {
                if (answer.Id <= 0)
                    continue;

                var entityAnswer = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.Context.CRF_TemplateAnswers.FirstOrDefault(x => x.CRFTemplateAnswerID == answer.Id);
                }); 
                if (entityAnswer != null)
                {
                    ansEntities.Add(entityAnswer);
                }
            }
            return ansEntities;
        }
    }
}

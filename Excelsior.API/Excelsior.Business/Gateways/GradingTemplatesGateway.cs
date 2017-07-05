using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class GradingTemplatesGateway : IGradingTemplatesGateway
    {
        public IGradingTemplatesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public GradingTemplatesGateway(IGradingTemplatesRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<GradingTemplateBaseDto>> GetAll(GradingTemplatesRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<GradingTemplateBaseDto>>();
            try
            {
                var templatesResult = new List<GradingTemplateBaseDto>();
                var templates = Repository.GetAll(request.StudyId, request.IsActive, request.IsLocked, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return templates.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var templatesPaged = GeneralHelper.GetPagedList(templates.OrderBy(x=> x.TrialID), result.Pager);
                if (templatesPaged != null)
                {
                    foreach (var entity in templatesPaged)
                    {
                        var dto = new GradingTemplateBaseDto(entity);
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

        public ResultInfo<GradingTemplateFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<GradingTemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.GTemplateID == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new GradingTemplateFullDto(entity);

                    GradingHelper.fillTemplateResult(Repository, entity, dto, true);

                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Grading Template not found");
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

        public ResultInfo<GradingTemplateFullDto> Add(GradingTemplateFullDto request)
        {

            //Perform input validation
            //----

            var result = new ResultInfo<GradingTemplateFullDto>();
            try
            {
                var entity = request.ToEntity();

                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new GradingTemplateFullDto(entity);
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

        public ResultInfo<GradingTemplateFullDto> Update(GradingTemplateFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<GradingTemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.GTemplateID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new GradingTemplateFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Grading Template not found");
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
                var entity = Repository.GetSingle(x => x.GTemplateID == id);
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
                    throw new Exception("Grading Template not found");
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
    }
}
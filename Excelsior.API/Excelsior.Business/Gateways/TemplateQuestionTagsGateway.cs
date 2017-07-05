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
    public class TemplateQuestionTagsGateway : ITemplateQuestionTagsGateway
    {
        public ITemplateQuestionTagsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public TemplateQuestionTagsGateway(ITemplateQuestionTagsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<TemplateQuestionTagBaseDto>> GetAll(TemplateQuestionTagsRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<TemplateQuestionTagBaseDto>>();
            try
            {
                var studyQuestionTagsRespose = new List<TemplateQuestionTagBaseDto>();
                var entities = Repository.GetAll(request.Name);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x => x.CRFTemplateQuestionTagID), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new TemplateQuestionTagFullDto(entity);
                        studyQuestionTagsRespose.Add(dto);
                    }
                }

                result.Result = studyQuestionTagsRespose;
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

        public ResultInfo<TemplateQuestionTagFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateQuestionTagFullDto>();
            try
            {
                var studyQuestionTag = Repository.GetSingle(x => x.CRFTemplateQuestionTagID == id);
                if (studyQuestionTag != null)
                {
                    var dto = new TemplateQuestionTagFullDto(studyQuestionTag);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question Tag not found");
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

        public ResultInfo<TemplateQuestionTagFullDto> Add(TemplateQuestionTagFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateQuestionTagFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new TemplateQuestionTagFullDto(entity);
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

        public ResultInfo<TemplateQuestionTagFullDto> Update(TemplateQuestionTagFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<TemplateQuestionTagFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateQuestionTagID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateQuestionTagFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question Tag not found");
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
                var studyQuestionTag = Repository.GetSingle(x => x.CRFTemplateQuestionTagID == id);
                if (studyQuestionTag != null)
                {
                    Repository.Delete(studyQuestionTag);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Question Tag not found");
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

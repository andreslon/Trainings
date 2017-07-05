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
    public class TemplateAnswersGateway : ITemplateAnswersGateway
    {
        public ITemplateAnswersRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public TemplateAnswersGateway(ITemplateAnswersRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<TemplateAnswerBaseDto>> GetAll(TemplateAnswersRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<TemplateAnswerBaseDto>>();
            try
            {
                var studyAnswersRespose = new List<TemplateAnswerBaseDto>();
                var studyAnswers = Repository.GetAll(request.QuestionId, request.Search);
                var count = 0;
                try
                {
                    count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return studyAnswers.Count();
                    }); 
                }
                catch (Exception e)
                { 
                }
                
                result.SetPager(count, request.Page, request.PageSize);
                var studyAnswersPaged = GeneralHelper.GetPagedList(studyAnswers.OrderBy(x => x.CRFTemplateAnswerID), result.Pager);
                if (studyAnswersPaged != null)
                {
                    foreach (var entity in studyAnswersPaged)
                    {
                        TemplateQuestionBaseDto q = null;
                        if (request.QuestionId != null)
                            q = new TemplateQuestionBaseDto();
                        var dto = new TemplateAnswerFullDto(entity, q);
                        studyAnswersRespose.Add(dto);
                    }
                }

                result.Result = studyAnswersRespose;
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

        public ResultInfo<TemplateAnswerFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateAnswerFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateAnswerID == id);
                if (entity != null)
                {
                    var dto = new TemplateAnswerFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template Answer not found");
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

        public ResultInfo<TemplateAnswerFullDto> Add(TemplateAnswerFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TemplateAnswerFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new TemplateAnswerFullDto(entity);
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

        public ResultInfo<TemplateAnswerFullDto> Update(TemplateAnswerFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<TemplateAnswerFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFTemplateAnswerID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TemplateAnswerFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Template Answer not found");
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
                var entity = Repository.GetSingle(x => x.CRFTemplateAnswerID == id);
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
                    throw new Exception("Template Answer not found");
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

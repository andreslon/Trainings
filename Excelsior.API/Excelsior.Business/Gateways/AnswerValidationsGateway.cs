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
    public class AnswerValidationsGateway : IAnswerValidationsGateway
    {
        public IAnswerValidationsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public AnswerValidationsGateway(IAnswerValidationsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<IList<AnswerValidationBaseDto>> GetAll(AnswerValidationsRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<AnswerValidationBaseDto>>();
            try
            {
                var answerTypesRespose = new List<AnswerValidationBaseDto>();
                var answerTypes = Repository.GetAll(request.Search);
                var count = 0;
                try
                {
                    count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return answerTypes.Count();
                    });
                }
                catch (Exception e)
                {
                }

                result.SetPager(count, request.Page, request.PageSize);
                var enitiesPaged = GeneralHelper.GetPagedList(answerTypes.OrderBy(x => x.Name), result.Pager);
                if (enitiesPaged != null)
                {
                    foreach (var entity in enitiesPaged)
                    {
                        var dto = new AnswerValidationBaseDto(entity);
                        answerTypesRespose.Add(dto);
                    }
                }

                result.Result = answerTypesRespose;
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

        public ResultInfo<AnswerValidationFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<AnswerValidationFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFAnswerValidationID == id);
                if (entity != null)
                {
                    var dto = new AnswerValidationFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Answer Validation not found");
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

        public ResultInfo<AnswerValidationFullDto> Add(AnswerValidationFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<AnswerValidationFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new AnswerValidationFullDto(entity);
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

        public ResultInfo<AnswerValidationFullDto> Update(AnswerValidationFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<AnswerValidationFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFAnswerValidationID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new AnswerValidationFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Answer Validation not found");
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
                var entity = Repository.GetSingle(x => x.CRFAnswerValidationID == id);
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
                    throw new Exception("Answer Validation not found");
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

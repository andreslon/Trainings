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
    public class CertQuestionsGateway : ICertQuestionsGateway
    {
        public ICertQuestionsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public CertQuestionsGateway(ICertQuestionsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<IList<CertQuestionBaseDto>> GetAll(CertQuestionsRequestDto request)
        {
            var result = new ResultInfo<IList<CertQuestionBaseDto>>();
            try
            {
                var respose = new List<CertQuestionBaseDto>();
                var entities = Repository.GetAll(request.ProcedureId, request.Search);
                var count = 0;
                try
                {
                    count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return entities.Count();
                    });
                }
                catch (Exception e)
                {
                }

                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x => x.CertQuestionDes), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new CertQuestionBaseDto(entity);
                        respose.Add(dto);
                    }
                }

                result.Result = respose;
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

        public ResultInfo<CertQuestionFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<CertQuestionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CertQuestionListID == id);
                if (entity != null)
                {
                    var dto = new CertQuestionFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Cert Question not found");
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

        public ResultInfo<CertQuestionFullDto> Add(CertQuestionFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<CertQuestionFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new CertQuestionFullDto(entity);
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

        public ResultInfo<CertQuestionFullDto> Update(CertQuestionFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<CertQuestionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CertQuestionListID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new CertQuestionFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Cert Question not found");
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
                var entity = Repository.GetSingle(x => x.CertQuestionListID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Cert Question not found");
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

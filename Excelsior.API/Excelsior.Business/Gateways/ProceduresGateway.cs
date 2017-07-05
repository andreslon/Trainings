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
    public class ProceduresGateway : IProceduresGateway
    {
        public IProceduresRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public ProceduresGateway(IProceduresRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<ProcedureBaseDto>> GetAll(ProceduresRequestDto request)
        {
            var result = new ResultInfo<IList<ProcedureBaseDto>>();
            try
            {
                var imageProceduresResponse = new List<ProcedureBaseDto>();

                IQueryable<CERT_ImgProcedureList> procedures = Repository.GetAll(request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return procedures.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var proceduresPaged = GeneralHelper.GetPagedList(procedures.OrderBy(x => x.DataTypeID), result.Pager);
                if (proceduresPaged != null)
                {
                    foreach (var proc in proceduresPaged)
                    {
                        var dto = new ProcedureBaseDto(proc);
                        imageProceduresResponse.Add(dto);
                    }
                }

                result.Result = imageProceduresResponse;
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

        public ResultInfo<ProcedureFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<ProcedureFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.ImgProcedureID == id);
                if (entity != null)
                {
                    var dto = new ProcedureFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Procedure not found");
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

        public ResultInfo<ProcedureFullDto> Add(ProcedureFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<ProcedureFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new ProcedureFullDto(entity);
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

        public ResultInfo<ProcedureFullDto> Update(ProcedureFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<ProcedureFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.ImgProcedureID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new ProcedureFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Procedure not found");
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
                var entity = Repository.GetSingle(x => x.ImgProcedureID == id);
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
                    throw new Exception("Procedure not found");
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

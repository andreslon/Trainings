using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Domain;
using Excelsior.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class MediaTypesGateway : IMediaTypesGateway
    {
        public IMediaTypesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public MediaTypesGateway(IMediaTypesRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<MediaTypeBaseDto>> GetAll(MediaTypesRequestDto request)
        {
            var result = new ResultInfo<IList<MediaTypeBaseDto>>();
            try
            {
                var dataTypesResponse = new List<MediaTypeBaseDto>();

                IQueryable<PACS_DataType> dataTypes = Repository.GetAll();
                foreach (var type in dataTypes)
                {
                    var dto = new MediaTypeBaseDto(type);
                    dataTypesResponse.Add(dto);
                }
                result.Result = dataTypesResponse;
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

        public ResultInfo<MediaTypeFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<MediaTypeFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DataTypeID == id);
                if (entity != null)
                {
                    var dto = new MediaTypeFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Data Type not found");
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

        public ResultInfo<MediaTypeFullDto> Add(MediaTypeFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<MediaTypeFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new MediaTypeFullDto(entity);
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

        public ResultInfo<MediaTypeFullDto> Update(MediaTypeFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<MediaTypeFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DataTypeID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new MediaTypeFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Data Type not found");
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
                var entity = Repository.GetSingle(x => x.DataTypeID == id);
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
                    throw new Exception("Data Type not found");
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

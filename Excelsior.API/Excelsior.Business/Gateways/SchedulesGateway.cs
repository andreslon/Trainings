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
    public class SchedulesGateway : ISchedulesGateway
    {
        public ISchedulesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public SchedulesGateway(ISchedulesRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<ScheduleBaseDto>> GetAll(SchedulesRequestDto request)
        {
            var result = new ResultInfo<IList<ScheduleBaseDto>>();
            try
            {
                var tpProcListResponse = new List<ScheduleBaseDto>();

                IQueryable<PACS_TPProcList> tpProcListItems = null;
                tpProcListItems = Repository.GetAll(request.StudyId, request.TimePointId, request.ProcedureId, request.SubjectId, request.Search).OrderBy(x => x.PACSTimePointsList.TimePointsSeq).ThenBy(x => x.CERTImgProcedureList.ImgProcedureName);

                var isOne = tpProcListItems.Count() == 1;

                foreach (var tpProcList in tpProcListItems)
                {
                    ScheduleBaseDto dto = null;
                    if (isOne)
                        dto = new ScheduleFullDto(tpProcList);
                    else
                        dto = new ScheduleBaseDto(tpProcList);

                    if (request.SubjectId.HasValue && request.ProcedureId.HasValue)
                    {
                        //Add the Series object
                        var seriesEntity = Repository.Context.WF_Sequences.FirstOrDefault(x => x.IsActive && x.PACSTimePoint.SubjectID == request.SubjectId
                            && x.PACSTPProcList.ImgProcedureID == request.ProcedureId
                            && x.PACSTimePoint.TimePointsListID == tpProcList.TimePointsListID);

                        if (seriesEntity != null)
                            dto.Series = new SeriesBaseDto(seriesEntity, dto);
                    }

                    tpProcListResponse.Add(dto);
                }

                result.Result = tpProcListResponse;
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

        public ResultInfo<ScheduleFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<ScheduleFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.TPProcID == id);
                if (entity != null)
                {
                    var dto = new ScheduleFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Schedule not found");
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

        public ResultInfo<ScheduleFullDto> Add(ScheduleFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<ScheduleFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new ScheduleFullDto(entity);
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

        public ResultInfo<ScheduleFullDto> Update(ScheduleFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<ScheduleFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.TPProcID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new ScheduleFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Schedule not found");
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
                var entity = Repository.GetSingle(x => x.TPProcID == id);
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
                    throw new Exception("Schedule not found");
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

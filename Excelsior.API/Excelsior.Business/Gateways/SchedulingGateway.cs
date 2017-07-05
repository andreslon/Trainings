using Excelsior.Business.DtoEntities;
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
    public class SchedulingGateway : ISchedulingGateway
    {
        public ISchedulingRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public SchedulingGateway(ISchedulingRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 
        public ResultInfo<IList<SchedulingProcedureFullDto>> GetProcedures(SchedulingProceduresRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<SchedulingProcedureFullDto>>();
            try
            {
                var proceduresResponse = new List<SchedulingProcedureFullDto>();

                var entities = Repository.GetProcedures(request.StudyId, request.Scheduled, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x => x.ImgProcedureName), result.Pager);
                if (entitiesPaged != null)
                {
                    var tpEntities = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return Repository.GetTimePointsList(request.StudyId).ToList();
                    });


                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new SchedulingProcedureFullDto(entity);

                        var tppEntities = DataHelpers.RetryPolicy.ExecuteAction(() =>
                        {
                            return entity.PACS_TPProcLists.Where(x => x.PACSTimePointsList.TrialID == request.StudyId).ToList();
                        });

                        foreach(var tpEntity in tpEntities)
                        {
                            var tppEntity = tppEntities.FirstOrDefault(x => x.TimePointsListID == tpEntity.TimePointsListID);
                            if(tppEntity != null)
                            {
                                dto.Schedules.Add(new SchedulingScheduleFullDto(tppEntity, dto));
                            }
                            else
                            {
                                var schedule = new SchedulingScheduleFullDto(new PACS_TPProcList()
                                {
                                    TimePointsListID = tpEntity.TimePointsListID,
                                    ImgProcedureID = entity.ImgProcedureID
                                }, dto);

                                schedule.TimePoint = new TimePointFullDto(tpEntity);
                                dto.Schedules.Add(schedule);
                            }
                        }

                        proceduresResponse.Add(dto);
                    }
                }

                result.Result = proceduresResponse;
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
    }
}